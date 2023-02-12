using System;
using System.Collections.Generic;
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
    private readonly IKnownInstanceRepo _knownInstanceRepo;
    private readonly ILogEntryRepo _logEntryRepo;
    private readonly ISessionRepo _sessionRepo;
    private readonly IUserAccessRepo _userAccessRepo;
    private readonly IUserRepo _userRepo;

    public SystemService(
        IRepositoryProvider provider)
    {
        _accessKeyRepo = provider.GetAccessKeyRepo();
        _configurationRepo = provider.GetConfigurationRepo();
        _knownInstanceRepo = provider.GetKnownInstanceRepo();
        _logEntryRepo = provider.GetLogEntryRepo();
        _sessionRepo = provider.GetSessionRepo();
        _userAccessRepo = provider.GetUserAccessRepo();
        _userRepo = provider.GetUserRepo();
    }

    public AccessKey GetAccessKey(Guid id)
    {
        return _accessKeyRepo.GetById(id);
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

    public User GetUserByEmail(string email)
    {
        return _userRepo.GetByEmail(email);
    }

    public void Save(User user)
    {
        _userRepo.Save(user);
    }

    public Session GetSession(Guid sessionId)
    {
        return _sessionRepo.GetById(sessionId);
    }

    public IEnumerable<Session> GetSessions(string email)
    {
        return _sessionRepo.GetByEmail(email);
    }

    public void Save(Session session)
    {
        _sessionRepo.Save(session);
    }

    public Dictionary<string, string> CreateSession(
        string email,
        string password)
    {
        var user = AuthenticateUser(email, password);

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
                { "SessionId", session.Id.ToString() },
                { "Email", user.Email },
                { "FamilyName", user.FamilyName },
                { "GivenName", user.GivenName },
                { "UserId", user.Id.ToString() },
                { "UserType", Enum.GetName(typeof(UserTypes), user.UserType) }
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

    public KnownInstance GetKnownInstance(Guid knownInstanceId)
    {
        return _knownInstanceRepo.GetById(knownInstanceId);
    }

    public void Save(KnownInstance knownInstance)
    {
        _knownInstanceRepo.Save(knownInstance);
    }
}
