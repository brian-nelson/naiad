using System;
using System.Collections.Generic;
using System.Data;
using LiteDB;
using Naiad.Libraries.System.Interfaces.DataManagement;
using Naiad.Libraries.System.Models.DataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.DataManagement;

public class LiteDbDataTableRepo : IDataTableRepo
{
    private readonly string _uniqueColumn;
    private readonly ILiteCollection<BsonDocument> _collection;

    public LiteDbDataTableRepo(
        ILiteDatabase database,
        StructuredDataDefinition structuredDataDefinition)
    {
        _uniqueColumn = structuredDataDefinition.IdentifierName;

        _collection = database.GetCollection(structuredDataDefinition.CollectionName);
        _collection.EnsureIndex(_uniqueColumn, true);
    }

    public DataTable GetData(int skip, int limit)
    {
        var dataTable = new DataTable();

        var bsonDocuments = _collection.Find(Query.All(_uniqueColumn), skip, limit);

        ProcessRows(dataTable, bsonDocuments);

        return dataTable;
    }

    public DataTable GetAllData()
    {
        var dataTable = new DataTable();

        var bsonDocuments = _collection.Find(Query.All(_uniqueColumn));

        ProcessRows(dataTable, bsonDocuments);

        return dataTable;
    }

    public void ProcessRows(DataTable dataTable, IEnumerable<BsonDocument> bsonDocuments)
    {
        foreach (var bsonDocument in bsonDocuments)
        {
            if (dataTable.Columns.Count == 0)
            {
                foreach (var bsonDocumentKey in bsonDocument.Keys)
                {
                    if (bsonDocumentKey.Equals("_id"))
                    {
                        // For now ignore the internal ids
                    }
                    else if (bsonDocumentKey.Equals(_uniqueColumn))
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
                var bsonValue = bsonDocument[bsonDocumentKey];

                if (bsonValue.Type != BsonType.ObjectId)
                {
                    dataRow[bsonDocumentKey] = bsonValue.AsString;
                }
            }

            dataTable.Rows.Add(dataRow);
        }
    }

    public void SaveData(DataTable table)
    {
        if (!table.Columns.Contains(_uniqueColumn))
        {
            throw new ArgumentException("Table does not contain specified unique identifier");
        }

        foreach (DataRow row in table.Rows)
        {
            var uniqueIdValue = row[_uniqueColumn].ToString();
            var existing = GetByUniqueColumn(uniqueIdValue);

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

    private BsonDocument GetByUniqueColumn(string uniqueIdValue)
    {
        return _collection.FindOne(Query.EQ(_uniqueColumn, uniqueIdValue));
    }
}