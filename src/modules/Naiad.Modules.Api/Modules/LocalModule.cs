using Autofac;
using Naiad.Libraries.Core.Objects;
using Naiad.Libraries.System.Interfaces;
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


        builder.RegisterBuildCallback(resolver =>
        {
            var IRepoProvider = resolver.Resolve<IRepositoryProvider>();

            
        });
    }
}