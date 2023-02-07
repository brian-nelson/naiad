using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LiteDB;
using Naiad.Libraries.System.Interfaces;

namespace Naiad.Libraries.System.LiteDb.Repos;

internal class InternalRepo<T>
    where T : IDbRecord, new()
{
    private readonly ILiteCollection<T> _collection;

    public InternalRepo(
        ILiteDatabase database,
        string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public void EnsureIndex<K>(Expression<Func<T, K>> keySelector, bool unique = false)
    {
        _collection.EnsureIndex(keySelector, unique);
    }


    public void Save(T obj)
    {
        if (obj.Id.Equals(Guid.Empty))
        {
            throw new Exception("Guid.Empty used as identifier");
        }

        _collection.Upsert(obj);
    }

    public T GetById(Guid id)
    {
        var obj = _collection.FindById(id);
        return obj;
    }

    public IEnumerable<T> GetItems(BsonExpression q, int skip = 0, int limit = Int32.MaxValue)
    {
        var items = _collection.Find(q, skip, limit);
        return items;
    }

    public T GetItem(BsonExpression q)
    {
        var item = _collection.FindOne(q);
        return item;
    }

    public IEnumerable<T> GetAll()
    {
        return _collection.FindAll()
            .ToList();
    }

    public void Delete(Guid id)
    {
        _collection.Delete(id);
    }
}
