using System.IO;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

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
    }
}
