using Naiad.Libraries.System.Constants.System;

namespace Naiad.Libraries.System.Models.System;

public class User
    : AbstractDbRecord
{
    private string _email;

    public string Email
    {
        get => _email;
        set => _email = value.ToLower();
    }

    public string GivenName { get; set; }

    public string FamilyName { get; set; }

    public bool IsEnabled { get; set; }

    public bool MustChangePassword { get; set; }

    public UserTypes UserType { get; set; }
}
