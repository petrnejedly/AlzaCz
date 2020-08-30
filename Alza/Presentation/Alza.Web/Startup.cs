using Alza.Application.Web.Facades;
using Alza.Domain.Abstractions.Repositories;
using Alza.Domain.Services;
using Alza.Infrastructure.SqlServer.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Alza.Web
{
    /// <summary>
    /// Startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> (v1) class.
        /// </summary>
        /// <param name="configuration">An instance of the application configuration properties.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">A collection of service descriptors.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddApiVersioning(config =>
            {
                config.ReportApiVersions = true;
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddAutoMapper(
                typeof(Startup),
                typeof(Infrastructure.SqlServer.MappingProfiles.ProductMappingProfile),
                typeof(Application.Web.MappingProfiles.ProductMappingProfile)
            );

            services.AddScoped(typeof(IMockedProductRepository), typeof(MockedProductRepository));
            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
            services.AddScoped(typeof(ProductService));
            services.AddScoped(typeof(ProductFacade));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v3", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Alza API v3",
                    Version = "v3",
                    Description = "A REST API service providing all available products of an eshop and enabling the partial update of one product.",
                });

                string xmlCommentsPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Alza.Web.xml");
                if (File.Exists(xmlCommentsPath))
                {
                    options.IncludeXmlComments(xmlCommentsPath);
                }
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Defines a class that provides the mechanism to configure an application's request pipeline.</param>
        /// <param name="env">Provides information about the web hosting environment an application is running in.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("v3/swagger.json", "Alza API v3"); //originally "./swagger/v1/swagger.json"
            });
        }
    }
}
