using Autofac;
using Naiad.Libraries.Core.Interfaces;
using Naiad.Libraries.Core.Objects;
using Naiad.Libraries.System.Logger;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.System.Services;
using Naiad.Libraries.Web.Helpers;
using Naiad.Modules.Api.Core.Objects;
using Naiad.Modules.Api.Core.Services;

namespace Naiad.Modules.Api.Core.Modules
{
    public class ApiModule : Module
    {
        private readonly Config _config;

        public ApiModule(Config config)
        {
            _config = config;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c =>
                {
                    var systemService = c.Resolve<SystemService>();
                    var configurationEntry = systemService.GetConfiguration("JWT_SECRET");
                    if (configurationEntry == null)
                    {
                        configurationEntry = new Configuration
                        {
                            Key = "JWT_SECRET",
                            Value = JwtHelper.CreateJwtSecret()
                        };

                        systemService.Save(configurationEntry);
                    }

                    var jwtSecret = new JwtSecret
                    {
                        Value = configurationEntry.Value
                    };

                    return jwtSecret;
                })
                .As<JwtSecret>();

            builder.RegisterType<MetadataUIService>();

            builder.RegisterType<SystemLogger>()
                .As<INaiadLogger>();
        }
    }
}
