using System.IO;
using System.Net;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Naiad.Libraries.Core.Helpers;
using Naiad.Libraries.Core.Objects;
using Naiad.Modules.Api;
using Newtonsoft.Json.Serialization;

namespace Naiad.modules.api
{
    class Program
    {
        public static Config Config;

        static void Main(string[] args)
        {
            Config = new Config(EnvironmentVariableHelper.GetMachineEnvVars("NAIAD_"));
            var contentRoot = Path.Combine(Directory.GetCurrentDirectory(), "webroot");

            var builder = new WebHostBuilder()
                .UseKestrel(options => options.AddServerHeader = false)
                .ConfigureServices(services =>
                {
                    services.AddOptions();

                    services
                        .AddMvc()
                        .AddApplicationPart(Assembly.Load(new AssemblyName("Naiad.Modules.Api.Core")));

                    services.AddControllers()
                        .AddNewtonsoftJson(options =>
                            options.SerializerSettings.ContractResolver = new DefaultContractResolver());

                    services.AddAutofac(c => { });

                    services.AddCors(options =>
                    {
                        options.AddPolicy(
                            "primary",
                            policy =>
                            {
                                policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                            });
                    });

                })
                .UseContentRoot(contentRoot)
                .UseStartup<Startup>()
                .ConfigureKestrel(serverOptions =>
                {
                    var port = Config.GetInt("API_PORT_NUMBER", 443);
                    var certificateFile = Config.GetString("API_CERTIFICATE_FILE");
                    var certificatePassword = Config.GetString("API_CERTIFICATE_PASSWORD");

                    serverOptions.Listen(IPAddress.Any, port, listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http2;
                        listenOptions.UseHttps(certificateFile, certificatePassword);
                    });
                });

            var host = builder.Build();
            host.Run();
        }
    }
}