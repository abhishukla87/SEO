using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SEOInfo.Factory;
using SEOInfo.Helper;
using SEOInfo.Service;

namespace SEOInfo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors();
            services.AddResponseCaching();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddScoped<SearchServiceFactory>();

            services.AddScoped<GoogleService>()
                        .AddScoped<ISearchService, GoogleService>(s => s.GetService<GoogleService>());

            services.AddScoped<BingService>()
                        .AddScoped<ISearchService, BingService>(s => s.GetService<BingService>());
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseResponseCaching();
            app.UseRouting();

            app.UseCors(x => x
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(origin => true)
              .AllowCredentials()); 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
