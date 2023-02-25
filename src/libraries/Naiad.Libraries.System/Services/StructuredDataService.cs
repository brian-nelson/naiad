using System;
using System.Data;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.Interfaces.DataManagement;
using Naiad.Libraries.System.Models.DataManagement;

namespace Naiad.Libraries.System.Services
{
    public class StructuredDataService
    {
        private readonly DataService _dataService;
        private readonly MetadataService _metadataService;
        private readonly IDataTableRepoFactory _dataTableRepoFactory;
        private readonly IConverterFactory _converterFactory;

        public StructuredDataService(
            DataService dataService,
            MetadataService metadataService,
            IDataTableRepoFactory dataTableRepoFactory,
            IConverterFactory converterFactory)
        {
            _dataService = dataService;
            _metadataService = metadataService;
            _dataTableRepoFactory = dataTableRepoFactory;
            _converterFactory = converterFactory;
        }

        public void TransformFileToStructuredData(
            Guid dataPointerId,
            Guid metadataId)
        {
            var sdd = _metadataService.GetStructuredDataDefinition(metadataId);

            if (sdd != null)
            {
                var dataPointer = _metadataService.GetDataPointer(dataPointerId);

                if (dataPointer != null)
                {
                    using (var stream = _dataService.GetFile(dataPointer.StorageLocation))
                    {
                        if (stream != null)
                        {
                            var converter = _converterFactory.GetConverter(sdd.MimeType);

                            if (converter != null)
                            {
                                var dataTable = converter.Convert(stream);

                                var repo = _dataTableRepoFactory.GetDataTableRepo(sdd);
                                repo.SaveData(dataTable);
                            }
                            else
                            {
                                throw new ArgumentException("No converter found for mimetype");
                            }
                        }
                        else
                        {
                            throw new ArgumentException("Data pointer found, but file not found");
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Data Pointer not found");
                }
            }
            else
            {
                throw new ArgumentException("Unknown type of structured data");
            }
        }

        public DataTable GetAllData(string structuredDataType)
        {
            var sdd = _metadataService.GetStructuredDataDefinition(structuredDataType);

            if (sdd == null)
            {
                throw new ArgumentException("Structured Data Definition not found");
            }

            return GetAllData(sdd);
        }

        public DataTable GetAllData(Guid metadataId)
        {
            var sdd = _metadataService.GetStructuredDataDefinition(metadataId);

            if (sdd == null)
            {
                throw new ArgumentException("Structured Data Definition not found");
            }

            return GetAllData(sdd);
        }

        public DataTable GetAllData(StructuredDataDefinition sdd)
        {
            var repo = _dataTableRepoFactory.GetDataTableRepo(sdd);
            return repo.GetAllData();
        }

    }
}
