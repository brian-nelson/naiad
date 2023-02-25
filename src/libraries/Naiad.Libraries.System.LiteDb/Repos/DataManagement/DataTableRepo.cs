using System;
using System.Data;
using LiteDB;
using Naiad.Libraries.System.Interfaces.DataManagement;
using Naiad.Libraries.System.Models.DataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.DataManagement
{
    public class DataTableRepo : IDataTableProvider
    {
        private readonly StructuredDataDefinition _structuredDataDefinition;

        private readonly string _uniqueColumn;
        private readonly ILiteCollection<BsonDocument> _collection;

        public DataTableRepo(
            ILiteDatabase database,
            StructuredDataDefinition structuredDataDefinition)
        {
            _structuredDataDefinition = structuredDataDefinition;

            _uniqueColumn = _structuredDataDefinition.IdentifierName;

            _collection = database.GetCollection(_structuredDataDefinition.CollectionName);
            _collection.EnsureIndex(_uniqueColumn, true);
        }
        
        public DataTable GetAllData()
        {
            var dataTable = new DataTable();

            var bsonDocuments = _collection.FindAll();

            foreach (var bsonDocument in bsonDocuments)
            {
                if (dataTable.Columns.Count == 0)
                {
                    foreach (var bsonDocumentKey in bsonDocument.Keys)
                    {
                        if (bsonDocumentKey.Equals(_uniqueColumn))
                        {
                            var column = dataTable.Columns.Add(bsonDocumentKey, typeof(string));
                            column.Unique = true;
                        }
                        else
                        {
                            dataTable.Columns.Add(bsonDocumentKey, typeof(string));
                        }
                        
                    }
                }

                var dataRow = dataTable.NewRow();

                foreach (var bsonDocumentKey in bsonDocument.Keys)
                {
                    dataRow[bsonDocumentKey] = bsonDocument[bsonDocumentKey];
                }

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        public void SaveData(DataTable table)
        {
            if (!table.Columns.Contains(_uniqueColumn))
            {
                throw new ArgumentException("Table does not contain specified unique identifier");
            }

            foreach (DataRow row in table.Rows)
            {
                var existing = GetByUniqueColumn(_uniqueColumn);

                if (existing == null)
                {
                    existing = new BsonDocument();
                }

                foreach (DataColumn dataColumn in table.Columns)
                {
                    var columnName = dataColumn.ColumnName;
                    existing[columnName] = row[columnName].ToString();
                }

                _collection.Upsert(existing);
            }
        }

        private BsonDocument GetByUniqueColumn(string rowId)
        {
            return _collection.FindOne(Query.EQ(_uniqueColumn, rowId));
        }
    }
}
