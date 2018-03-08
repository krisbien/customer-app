using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CustomerWebApi.Services;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using CustomerWebApi.Interfaces;

namespace CustomerWebApi
{
    public class Startup
    {
        public virtual void InitializeCustomerContext(IServiceCollection services)
        {
            services.AddDbContext<CustomerContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("Default")));
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InitializeCustomerContext(services);
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Customer API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseMvc(routes => {

                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}/{id?}");

               
                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
               
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API V1");
            });
        }
    }
}
