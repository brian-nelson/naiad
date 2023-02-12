using Autofac;
using LiteDB;
using Naiad.Libraries.Core.Objects;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.Interfaces.DataManagement;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.LiteDb.Providers;
using Naiad.Libraries.System.LiteDb.Repos.DataManagement;
using Naiad.Libraries.System.LiteDb.Repos.MetadataManagement;
using Naiad.Libraries.System.LiteDb.Repos.System;

namespace Naiad.Libraries.System.LiteDb.Modules;

public class LiteDbModule : Module, IRepositoryProviderModule
{
    private Config _config;

    public void SetConfig(Config config)
    {
        _config = config;
    }

    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.Register<ILiteDatabase>(c =>
            {
                var connectionString = _config.GetString("API_DATA_FILE");

                var liteDb = new LiteDatabase(connectionString);
                return liteDb;
            })
            .SingleInstance();

        // Register Repo Provider
        builder.RegisterType<LiteDbRepositoryProvider>()
            .As<IRepositoryProvider>()
            .SingleInstance();

        // Data Management 
        builder.RegisterType<StorageProvider>()
            .As<IStorageProvider>();

        // Metadata Management
        builder.RegisterType<CategorizationRepo>()
            .As<ICategorizationRepo>();
        builder.RegisterType<DataPointerRepo>()
            .As<IDataPointerRepo>();
        builder.RegisterType<GranularityRepo>()
            .As<IGranularityRepo>();
        builder.RegisterType<MetadataPropertyRepo>()
            .As<IMetadataPropertyRepo>();
        builder.RegisterType<MetadataRepo>()
            .As<IMetadataRepo>();
        builder.RegisterType<RelationshipRepo>()
            .As<IRelationshipRepo>();
        builder.RegisterType<ZoneRepo>()
            .As<IZoneRepo>();

        // System Management
        builder.RegisterType<AccessKeyRepo>()
            .As<IAccessKeyRepo>();
        builder.RegisterType<ConfigurationRepo>()
            .As<IConfigurationRepo>();
        builder.RegisterType<KnownInstanceRepo>()
            .As<IKnownInstanceRepo>();
        builder.RegisterType<LogEntryRepo>()
            .As<ILogEntryRepo>();
        builder.RegisterType<SessionRepo>()
            .As<ISessionRepo>();
        builder.RegisterType<UserAccessRepo>()
            .As<IUserAccessRepo>();
        builder.RegisterType<UserRepo>()
            .As<IUserRepo>();

    }
}
