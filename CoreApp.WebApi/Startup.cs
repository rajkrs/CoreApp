using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CoreApp.IpWhitelist;
using CoreApp.ModelStateValidation;
using CoreApp.ApiCache;

namespace CoreApp.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IpWhitelistFilter>();

            services.AddMvcCore(options =>
            {
                //options.Filters.Add(typeof(IpWhitelistFilter))
            }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonFormatters()
            .AddXmlSerializerFormatters();

 

            services.UseCoreAppCustomModelValidation();
            //services.UseCoreAppDistributedCaching(new CustomCachingConfig());
            services.AddCoreAppDistributedCaching(new SqlCachingConfig());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseIpWhitelist(Configuration["IpWhitelist"]);
            //app.UseIpWhitelist((new string[] { "10.131.96.64", "::1" }).ToList());


            app.UseHttpsRedirection();
            app.UseCoreAppDistributedCaching();
            app.UseMvc();

        }
    }
}
