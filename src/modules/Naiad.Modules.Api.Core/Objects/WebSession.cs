using System;
using Naiad.Modules.Api.Core.Interfaces;

namespace Naiad.Modules.Api.Core.Objects
{
    public class WebSession : IWebSession
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserType { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
    }
}
