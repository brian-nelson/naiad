using System;

namespace Naiad.Modules.Api.Core.Objects;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string GivenName { get; set; }
    public string FamilyName { get; set; }
    public string UserRole { get; set; }
    public string Username { get; set; }
    public bool IsEnabled { get; set; }
    public bool MustChangePassword { get; set; }
}