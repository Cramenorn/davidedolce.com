using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyWebsite.Models;
using MyWebsite.Models.ViewModels;
using MyWebsite.BusinessLogic.SendGrid;
using MyWebsite.BusinessLogic.IArticles;

namespace MyWebsite
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json")
                .Build());

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
            services.AddResponseCompression(options => {
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddOptions();

            services.Configure<SendGridModel>(Configuration.GetSection("SendGrid"));

            services.AddSingleton<IArticles, FakeArticleRepository>();
            services.AddSingleton<ArticleListViewModel>();
            services.AddSingleton<SendGridEmail>();
            services.AddSingleton<PagingInfo>();

            services.AddMvc().AddXmlSerializerFormatters().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseStatusCodePages();
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            else
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = new PwaContentTypeProvider()
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "paginationBlog",
                    template: "Blog/Page{articlePage}",
                    defaults: new { Controller = "Home", action = "Blog" });
                routes.MapRoute("blog", "{controller=Blog}/{action}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}");
            });
        }
    }
    public class PwaContentTypeProvider : FileExtensionContentTypeProvider
    {
        public PwaContentTypeProvider()
        {
            Mappings.Add(".webmanifest", "application/manifest+json");
        }
    }
}
