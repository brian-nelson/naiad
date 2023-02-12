using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Naiad.Libraries.Core.Helpers;
using Naiad.Libraries.Core.Objects;
using Naiad.Libraries.System.Services;
using Naiad.Modules.Api;
using Naiad.Modules.Api.Core.Objects;
using Newtonsoft.Json.Serialization;

namespace Naiad.modules.api
{
    class Program
    {
        public static Config Config;
        public static JwtSecret JwtSecret;

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

                    services.AddAutofac(builder => { });

                    services.AddAuthentication(x =>
                        {
                            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        })
                        .AddJwtBearer(options =>
                        {
                            options.RequireHttpsMetadata = false;
                            options.SaveToken = true;
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(JwtSecret.Value)),
                                ValidateIssuer = false,
                                ValidateAudience = false
                            };
                            options.Events = new JwtBearerEvents
                            {
                                OnAuthenticationFailed = context =>
                                {
                                    return Task.CompletedTask;
                                }
                            };
                        });

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
                        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                        listenOptions.UseHttps(certificateFile, certificatePassword);
                    });
                });

            var host = builder.Build();

            JwtSecret = host.Services.GetRequiredService<JwtSecret>();
            var bootstrap = host.Services.GetRequiredService<BootstrapService>();
            bootstrap.PerformBootstrap();

            host.Run();
        }
    }
}