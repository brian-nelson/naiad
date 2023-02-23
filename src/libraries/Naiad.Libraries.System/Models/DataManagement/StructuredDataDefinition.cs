using System;

namespace Naiad.Libraries.System.Models.DataManagement
{
    public class StructuredDataDefinition
    {
        public Guid MetadataId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MimeType { get; set; }

        public string IdentifierName { get; set; }

        public string CollectionName { get; set; }
    }
}
