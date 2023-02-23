using System;

namespace Naiad.Modules.Api.Core.Objects
{
    public class StructuredDataDto
    {
        public Guid MetadataId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MimeType { get; set; }

        public string IdentifierName { get; set; }
    }
}
