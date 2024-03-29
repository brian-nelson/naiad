﻿using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;

public class DataPointerRepo : IDataPointerRepo
{
    private readonly BaseRepo<DataPointer> _repo;

    public DataPointerRepo(
        ILiteDatabase database)
    {
        _repo = new BaseRepo<DataPointer>(database, "datapointers");

        _repo.EnsureIndex(x => x.GranularityId);
        _repo.EnsureIndex(x => x.ZoneId);
        _repo.EnsureIndex(x => x.StorageLocation, true);
    }

    public DataPointer GetById(Guid id)
    {
        return _repo.GetById(id);
    }

    public DataPointer GetByLocation(string fileId)
    {
        return _repo.GetItem(
            Query.EQ("StorageLocation", fileId));
    }

    public void Save(DataPointer pointer)
    {
        _repo.Save(pointer);
    }

    public IEnumerable<DataPointer> GetAll()
    {
        return _repo.GetAll();
    }

    public IEnumerable<DataPointer> GetByZone(Guid zoneId)
    {
        return _repo.GetItems(
            Query.EQ("ZoneId", zoneId));
    }

    public IEnumerable<DataPointer> GetByGranularity(Guid granularityId)
    {
        return _repo.GetItems(
            Query.EQ("GranularityId", granularityId));
    }

    public IEnumerable<DataPointer> GetByZoneAndGranularity(Guid zoneId, Guid granularityId)
    {
        return _repo.GetItems(
            Query.And(
            Query.EQ("ZoneId", zoneId),
            Query.EQ("GranularityId", granularityId)));
    }
}

