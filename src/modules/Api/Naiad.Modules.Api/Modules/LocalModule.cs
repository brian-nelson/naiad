using Autofac;
using Naiad.Libraries.Core.Objects;
using Naiad.Libraries.System.Converters;
using Naiad.Libraries.System.Factories;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.Interfaces.DataManagement;
using Naiad.Libraries.System.LiteDb.Factory;
using Naiad.Libraries.System.Services;

namespace Naiad.Modules.Api.Modules;

public class LocalModule : Module
{
    private readonly Config _config;

    public LocalModule(Config config)
    {
        _config = config;
    }

    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<DataService>();
        builder.RegisterType<SystemService>();
        builder.RegisterType<MetadataService>();
        builder.RegisterType<BootstrapService>();
        builder.RegisterType<StructuredDataService>();

        builder.RegisterType<CsvConverter>()
            .As<IDataTableConverter>();

        builder.RegisterType<ConverterFactory>()
            .As<IConverterFactory>();

        builder.RegisterType<LiteDbDataTableRepoFactory>()
            .As<IDataTableRepoFactory>();


        builder.RegisterBuildCallback(resolver =>
        {
            var IRepoProvider = resolver.Resolve<IRepositoryProvider>();

            
        });
    }
}