namespace Naiad.Libraries.System.Models.System
{
    public class User : AbstractDbRecord
    {
        public string Email { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public bool IsEnabled { get; set; }

        public bool MustChangePassword { get; set; }
    }
}
