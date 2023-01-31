using System;
using Naiad.Libraries.System.Constants.MetadataManagement;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.Models.MetadataManagement
{
    public class Relationship : AbstractDbRecord
    {
        public Guid ParentId { get; set; }
        public EntityType ParentType { get; set; }
        
        public Guid ChildId { get; set; }
        public EntityType ChildType { get; set; }

        public string ConnectionContext { get; set; }
    }
}
