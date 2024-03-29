﻿using LiteDB;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.Interfaces.DataManagement;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.LiteDb.Repos.DataManagement;
using Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;
using Naiad.Libraries.System.LiteDb.Repos.System;
using NotImplementedException = System.NotImplementedException;

namespace Naiad.Libraries.System.LiteDb.Providers;

public class LiteDbRepositoryProvider : IRepositoryProvider
{
    internal ILiteDatabase Database { get; }

    public LiteDbRepositoryProvider(
        ILiteDatabase database)
    {
        Database = database;

        var mapper = BsonMapper.Global;

        // Metadata
        mapper.Entity<IDbRecord>()
            .Id(x => x.Id);
    }

    public IStorageRepo GetStorageProvider()
    {
        return new LiteDbStorageRepo(Database);
    }

    public ICategorizationRepo GetCategorizationRepo()
    {
        return new CategorizationRepo(Database);
    }

    public IDataPointerRepo GetDataPointerRepo()
    {
        return new DataPointerRepo(Database);
    }

    public IGranularityRepo GetGranularityRepo()
    {
        return new GranularityRepo(Database);
    }

    public IMetadataPropertyRepo GetMetadataPropertyRepo()
    {
        return new MetadataPropertyRepo(Database);
    }

    public IMetadataRepo GetMetadataRepo()
    {
        return new MetadataRepo(Database);
    }

    public IRelationshipRepo GetRelationshipRepo()
    {
        return new RelationshipRepo(Database);
    }

    public IZoneRepo GetZonRepo()
    {
        return new ZoneRepo(Database);
    }

    public IAccessKeyRepo GetAccessKeyRepo()
    {
        return new AccessKeyRepo(Database);
    }

    public IConfigurationRepo GetConfigurationRepo()
    {
        return new ConfigurationRepo(Database);
    }

    public IHandlerConfigurationRepo GetHandlerConfigurationRepo()
    {
        return new HandlerConfigurationRepo(Database);
    }

    public IKnownInstanceRepo GetKnownInstanceRepo()
    {
        return new KnownInstanceRepo(Database);
    }

    public ILogEntryRepo GetLogEntryRepo()
    {
        return new LogEntryRepo(Database);
    }

    public ISessionRepo GetSessionRepo()
    {
        return new SessionRepo(Database);
    }

    public ISystemEventRepo GetSystemEventRepo()
    {
        return new SystemEventRepo(Database);
    }

    public ISystemEventReceiptRepo GetSystemEventReceiptRepo()
    {
        return new SystemEventReceiptRepo(Database);
    }

    public IUserAccessRepo GetUserAccessRepo()
    {
        return new UserAccessRepo(Database);
    }

    public IUserRepo GetUserRepo()
    {
        return new UserRepo(Database);
    }
}