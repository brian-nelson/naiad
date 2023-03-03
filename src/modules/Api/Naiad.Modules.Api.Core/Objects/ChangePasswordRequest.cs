namespace Naiad.Modules.Api.Core.Objects;

public class ChangePasswordRequest
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}
