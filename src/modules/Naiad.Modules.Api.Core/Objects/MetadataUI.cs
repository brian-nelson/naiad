using System;
using System.Collections.Generic;

namespace Naiad.Modules.Api.Core.Objects
{
    public class MetadataUI
    {
        public MetadataUI()
        {
            Properties = new Dictionary<string, string>();
        }

        public Guid Id { get; set; }

        public Guid? CategorizationId { get; set; }

        public Dictionary<string, string> Properties { get; set; }

        public string ConnectionContext { get; set; }
    }
}
