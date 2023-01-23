using System;
using Naiad.Libraries.System.Interfaces;

namespace Naiad.Libraries.System.Objects
{
    public class User : IDbRecord
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public string HashedPassword { get; set; }
    }
}
