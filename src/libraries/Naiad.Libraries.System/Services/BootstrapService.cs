using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naiad.Libraries.Core.Helpers;
using Naiad.Libraries.System.Constants.System;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.Services;

public class BootstrapService
{
    private readonly IUserRepo _userRepo;
    private readonly IUserAccessRepo _userAccessRepo;

    public BootstrapService(
        IUserRepo userRepo,
        IUserAccessRepo userAccessRepo)
    {
        _userRepo = userRepo;
        _userAccessRepo = userAccessRepo;
    }

    public void PerformBootstrap()
    {
        var admins = _userRepo.GetByType(UserTypes.Administrator);

        User enabledAdmin = null;

        foreach (var admin in admins)
        {
            if (admin.IsEnabled)
            {
                enabledAdmin = admin;
                break;
            }
        }

        if (enabledAdmin == null)
        {
            enabledAdmin = new User
            {
                Email = "admin@naiad.local",
                GivenName = "System",
                FamilyName = "Admin",
                IsEnabled = true,
                UserType = UserTypes.Administrator,
                MustChangePassword = false
            };
            _userRepo.Save(enabledAdmin);

            Console.WriteLine($"Default Admin Username - {enabledAdmin.Email}");
        }

        var userAccess = _userAccessRepo.GetByUserId(enabledAdmin.Id);
        if (userAccess == null)
        {
            string password = PasswordHelper.GeneratePassword(PasswordHelper.DefaultAdminPasswordLength);
            string salt = PasswordHelper.GenerateSalt();

            userAccess = new UserAccess
            {
                UserId = enabledAdmin.Id,
                SetOnDateTime = DateTimeOffset.UtcNow,
                Salt = salt,
                HashedPassword = PasswordHelper.Hash(password, salt)
            };
            _userAccessRepo.Save(userAccess);

            Console.WriteLine($"Default Admin Password - {password}");
        }
    }
}

