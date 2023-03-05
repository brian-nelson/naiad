using System;
using System.IO;
using System.Text;
using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.LiteDb.Modules;
using Naiad.Modules.ActivityPub.Core.Modules;
using Naiad.modules.api;
using Naiad.Modules.Api.Core.Modules;
using Naiad.Modules.Api.Modules;

namespace Naiad.Modules.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath);

            Configuration = builder.Build();
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "text/html";

                    var sb = new StringBuilder();
                    sb.Append("<html><body>");
                    sb.Append("<p>A server error has occurred</p>");

                    var exceptionHandler = context.Features.Get<IExceptionHandlerPathFeature>();
                    var error = exceptionHandler?.Error;

                    if (error != null)
                    {
                        sb.Append($"<p>{error.Message}</p>");
                    }

                    sb.Append("</body></html>");
                    await context.Response.WriteAsync(sb.ToString());
                });
            });
            
            string staticContentFolder = Path.Combine(Directory.GetCurrentDirectory(), "webroot");

            DefaultFilesOptions defaultFileOptions = new DefaultFilesOptions();
            defaultFileOptions.DefaultFileNames.Clear();
            defaultFileOptions.DefaultFileNames.Add("index.html");
            defaultFileOptions.FileProvider = new PhysicalFileProvider(staticContentFolder);
            defaultFileOptions.RequestPath = new PathString("");
            app.UseDefaultFiles(defaultFileOptions);

            app.UseRouting();

            app.UseCors("primary");
            
            StaticFileOptions staticFileOptions = new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticContentFolder),
                RequestPath = new PathString(""),
                ContentTypeProvider = new FileExtensionContentTypeProvider(),
                DefaultContentType = "application/javascript"
            };
            app.UseStaticFiles(staticFileOptions);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var config = Program.Config;
               
            builder.RegisterModule(
                new LocalModule(config));

            builder.RegisterModule(
                new ApiModule(config));

            builder.RegisterModule(
                new ApModule(config));

            //var repositoryProviderAssemblyName = config.GetString("API_REPO_PROVIDER_ASSEMBLY");
            var repositoryProviderModuleName = config.GetString("API_REPO_PROVIDER_MODULE");

            IRepositoryProviderModule repositoryProviderModule = null;

            // TODO - This should not require Naiad to know about the libraries in advance
            if (repositoryProviderModuleName.Equals("Naiad.Libraries.System.LiteDb.Modules.LiteDbModule"))
            {
                repositoryProviderModule = new LiteDbModule();
                repositoryProviderModule.SetConfig(config);
            }

            if (repositoryProviderModule is IModule module)
            {
                builder.RegisterModule(module);
            }
            else
            {
                throw new Exception($"{repositoryProviderModuleName} does not implement IModule");
            }
        }
    }
}
