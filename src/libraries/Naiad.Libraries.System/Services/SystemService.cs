using Naiad.Libraries.System.Interfaces.System;

namespace Naiad.Libraries.System.Services;

public class SystemService
{
    private readonly IAccessKeyRepo _accessKeyRepo;
    private readonly ICertificateRepo _certificateRepo;
    private readonly IKnownInstanceRepo _knownInstanceRepo;
    private readonly ILogEntryRepo _logEntryRepo;
    private readonly IUserAccessRepo _userAccessRepo;
    private readonly IUserRepo _userRepo;

    public SystemService(
        IAccessKeyRepo accessKeyRepo,
        ICertificateRepo certificateRepo,
        IKnownInstanceRepo knownInstanceRepo,
        ILogEntryRepo logEntryRepo,
        IUserAccessRepo userAccessRepo,
        IUserRepo userRepo)
    {
        _accessKeyRepo = accessKeyRepo;
        _certificateRepo = certificateRepo;
        _knownInstanceRepo = knownInstanceRepo;
        _logEntryRepo = logEntryRepo;
        _userAccessRepo = userAccessRepo;
        _userRepo = userRepo;
    }


}
