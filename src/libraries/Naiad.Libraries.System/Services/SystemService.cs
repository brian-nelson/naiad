using System;
using System.Collections.Generic;
using System.Security.Claims;
using Naiad.Libraries.Core.Helpers;
using Naiad.Libraries.System.Constants.System;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.Services;

public class SystemService
{
    private readonly IAccessKeyRepo _accessKeyRepo;
    private readonly IConfigurationRepo _configurationRepo;
    private readonly IConnectorConfigurationRepo _connectorConfigurationRepo;
    private readonly IKnownInstanceRepo _knownInstanceRepo;
    private readonly ISessionRepo _sessionRepo;
    private readonly IUserAccessRepo _userAccessRepo;
    private readonly IUserRepo _userRepo;

    public SystemService(
        IRepositoryProvider provider)
    {
        _accessKeyRepo = provider.GetAccessKeyRepo();
        _configurationRepo = provider.GetConfigurationRepo();
        _connectorConfigurationRepo = provider.GetConnectorConfigurationRepo();
        _knownInstanceRepo = provider.GetKnownInstanceRepo();
        _sessionRepo = provider.GetSessionRepo();
        _userAccessRepo = provider.GetUserAccessRepo();
        _userRepo = provider.GetUserRepo();
    }

    public AccessKey GetAccessKey(Guid id)
    {
        return _accessKeyRepo.GetById(id);
    }

    public AccessKey GetAccessKey(string key)
    {
        return _accessKeyRepo.GetByKey(key);
    }

    public IEnumerable<AccessKey> GetAccessKeys(Guid userId)
    {
        return _accessKeyRepo.GetByUserId(userId);
    }

    public void Save(AccessKey accessKey)
    {
        _accessKeyRepo.Save(accessKey);
    }

    public UserAccess GetUserAccess(Guid id)
    {
        return _userAccessRepo.GetById(id);
    }

    public void SetPassword(
        Guid userId, 
        string newPassword)
    {
        var userAccess = _userAccessRepo.GetByUserId(userId);

        if (userAccess == null)
        {
            string salt = PasswordHelper.GenerateSalt();

            userAccess = new UserAccess
            {
                UserId = userId,
                SetOnDateTime = DateTimeOffset.UtcNow,
                Salt = salt,
                HashedPassword = PasswordHelper.Hash(newPassword, salt)
            };
            _userAccessRepo.Save(userAccess);
        }

        CloseAllSessions(userId);
    }

    public void CloseAllSessions(Guid userId)
    {
        var sessions = _sessionRepo.GetByUser(userId);

        foreach (var session in sessions)
        {
            session.IsDeleted = true;
            _sessionRepo.Save(session);
        }
    }

    public void CreateAccessKey(
        Guid userId,
        string key,
        string secretKey)
    {
        var accessKey = new AccessKey
        {
            Key = key,
            Salt = PasswordHelper.GenerateSalt(),
            CreatedDateTime = DateTimeOffset.UtcNow,
            UserId = userId,
            IsEnabled = true
        };

        accessKey.HashedSecret = PasswordHelper.Hash(secretKey, accessKey.Salt);

        _accessKeyRepo.Save(accessKey);
    }

    public void ChangePassword(
        Guid userId,
        string oldPassword,
        string newPassword)
    {
        var userAccess = _userAccessRepo.GetByUserId(userId);

        if (userAccess != null)
        {
            var testHash = PasswordHelper.Hash(oldPassword, userAccess.Salt);

            if (testHash.Equals(userAccess.HashedPassword))
            {
                string salt = PasswordHelper.GenerateSalt();

                userAccess.SetOnDateTime = DateTimeOffset.UtcNow;
                userAccess.Salt = salt;
                userAccess.HashedPassword = PasswordHelper.Hash(newPassword, salt);

                _userAccessRepo.Save(userAccess);
            }

            return;
        }

        throw new Exception("Password not initially set");
    }

    public UserAccess GetUserAccessByUser(Guid userId)
    {
        return _userAccessRepo.GetByUserId(userId);
    }

    public void Save(UserAccess userAccess)
    {
        _userAccessRepo.Save(userAccess);
    }

    public User GetUser(Guid userId)
    {
        return _userRepo.GetById(userId);
    }

    public IEnumerable<User> GetUsers()
    {
        return _userRepo.GetAll();
    }

    public User GetUserByEmail(string email)
    {
        return _userRepo.GetByEmail(email);
    }

    public User GetUserByUsername(string username)
    {
        return _userRepo.GetByUsername(username);
    }

    public void Save(User user)
    {
        _userRepo.Save(user);
    }

    public Session GetSession(Guid sessionId)
    {
        return _sessionRepo.GetById(sessionId);
    }

    public IEnumerable<Session> GetOpenSessions(Guid userId)
    {
        return _sessionRepo.GetByUser(userId);
    }

    public void Save(Session session)
    {
        _sessionRepo.Save(session);
    }

    public bool ValidateSession(Guid sessionId)
    {
        var session = _sessionRepo.GetById(sessionId);

        if (session != null)
        {
            if (!session.IsDeleted)
            {
                var now = DateTimeOffset.UtcNow;

                if (now >= session.CreatedOnDateTime
                    && now <= session.ExpiresOnDateTime)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public Dictionary<string, string> CreateSession(
        string email,
        string password,
        out User user)
    {
        user = AuthenticateUser(email, password);

        if (user != null)
        {
            var now = DateTimeOffset.UtcNow;

            var session = new Session
            {
                UserId = user.Id,
                IsDeleted = false,
                CreatedOnDateTime = now,
                ExpiresOnDateTime = now.AddDays(30)
            };
            Save(session);

            Dictionary<string, string> sessionValues = new Dictionary<string, string>
            {
                { "UserId", user.Id.ToString() },
                { "SessionId", session.Id.ToString() },
                { "FamilyName", user.FamilyName },
                { "GivenName", user.GivenName },
                { ClaimTypes.Name, user.Email },
                { ClaimTypes.Role, Enum.GetName(typeof(UserTypes), user.UserType) }
            };

            return sessionValues;
        }

        return null;
    }

    public Dictionary<string, string> CreateSessionFromAccessKey(
        string key,
        string secretKey,
        out User user)
    {
        user = AuthenticateAccessKey(key, secretKey);

        if (user != null)
        {
            var now = DateTimeOffset.UtcNow;

            var session = new Session
            {
                UserId = user.Id,
                IsDeleted = false,
                CreatedOnDateTime = now,
                ExpiresOnDateTime = now.AddDays(30)
            };
            Save(session);

            Dictionary<string, string> sessionValues = new Dictionary<string, string>
            {
                { "UserId", user.Id.ToString() },
                { "SessionId", session.Id.ToString() },
                { "FamilyName", user.FamilyName },
                { "GivenName", user.GivenName },
                { ClaimTypes.Name, user.Email },
                { ClaimTypes.Role, Enum.GetName(typeof(UserTypes), user.UserType) }
            };

            return sessionValues;
        }

        return null;
    }

    public void CloseSession(Guid sessionId)
    {
        var session = _sessionRepo.GetById(sessionId);
        session.IsDeleted = true;
        _sessionRepo.Save(session);
    }

    public User AuthenticateUser(
        string email,
        string password)
    {
        var user = _userRepo.GetByEmail(email);

        if (user != null)
        {
            var userAccess = _userAccessRepo.GetByUserId(user.Id);

            if (userAccess != null)
            {
                var testHash = PasswordHelper.Hash(password, userAccess.Salt);

                if (testHash.Equals(userAccess.HashedPassword))
                {
                    return user;
                }
            }
        }

        return null;
    }

    public User AuthenticateAccessKey(
        string key,
        string secretKey)
    {
        var accessKey = _accessKeyRepo.GetByKey(key);
        var user = _userRepo.GetById(accessKey.UserId);

        if (user != null)
        {
            var testHash = PasswordHelper.Hash(secretKey, accessKey.Salt);

            if (testHash.Equals(accessKey.HashedSecret))
            {
                return user;
            }
        }

        return null;
    }

    // Naiad Configuration

    public IEnumerable<Configuration> GetConfigurations()
    {
        return _configurationRepo.GetAll();
    }

    public IEnumerable<Configuration> GetExternalConfigurations()
    {
        return _configurationRepo.GetAllExternal();
    }

    public Configuration GetConfiguration(Guid configurationId)
    {
        return _configurationRepo.GetById(configurationId);
    }

    public Configuration GetConfiguration(string key)
    {
        return _configurationRepo.GetByKey(key);
    }

    public void Save(Configuration configuration)
    {
        _configurationRepo.Save(configuration);
    }

    // Known Instance

    public KnownInstance GetKnownInstance(Guid knownInstanceId)
    {
        return _knownInstanceRepo.GetById(knownInstanceId);
    }

    public void Save(KnownInstance knownInstance)
    {
        _knownInstanceRepo.Save(knownInstance);
    }

    // Connector Configuration

    public IEnumerable<ConnectorConfiguration> GetConnectorConfigurations()
    {
        return _connectorConfigurationRepo.GetAll();
    }

    public void Save(ConnectorConfiguration config)
    {
        _connectorConfigurationRepo.Save(config);
    }
}
