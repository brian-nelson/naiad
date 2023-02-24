using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naiad.Libraries.System.Interfaces.MetadataManagement;

namespace Naiad.Libraries.System.Services
{
    public class StructuredDataService
    {
        private readonly DataService _dataService;
        private readonly MetadataService _metadataService;

        public StructuredDataService(
            DataService dataService,
            MetadataService metadataService)
        {
            _dataService = dataService;
            _metadataService = metadataService;
        }

        public void TransformFileToStructuredData()
        {

        }


    }
}
