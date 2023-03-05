using Autofac;
using Naiad.Libraries.ActivityPub.Models.Environment;
using Naiad.Libraries.ActivityPub.Services;
using Naiad.Libraries.Core.Objects;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.System.Services;

namespace Naiad.Modules.ActivityPub.Core.Modules
{
    public class ApModule : Module
    {
        private readonly Config _config;

        public ApModule(Config config)
        {
            _config = config;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ApService>();

            builder.Register(c =>
                {
                    var systemService = c.Resolve<SystemService>();
                    var configurationEntry = systemService.GetConfiguration("DOMAIN_NAME");
                    if (configurationEntry == null)
                    {
                        configurationEntry = new Configuration
                        {
                            Key = "DOMAIN_NAME",
                            Value = "localhost"
                        };

                        systemService.Save(configurationEntry);
                    }

                    var domainName = new DomainName()
                    {
                        Value = configurationEntry.Value
                    };

                    return domainName;
                })
                .As<DomainName>();
        }
    }
}
