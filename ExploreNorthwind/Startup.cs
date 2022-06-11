using ExploreNorthwind.ConfigurationOptions;
using ExploreNorthwindDataAccess.NorthwindDB;
using ExploreNorthwindDataAccess.Repositories;
using ExploreNorthwindDataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace ExploreNorthwind
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
            services.AddControllersWithViews();

            var exploreNorthwindOptions = new ExploreNorthwindOptions();
            Configuration.GetSection(ExploreNorthwindOptions.ExploreNorthwindOptionsName).Bind(exploreNorthwindOptions);
            
            services.AddDbContext<INorthwindContext, NorthwindContext>(options => options.UseSqlServer(exploreNorthwindOptions.NorthwindConnectionString));
            services.AddTransient<ICategoriesRepository, CategoriesRepository>();
            services.AddTransient<IProductsRepository, ProductsRepository>();
            services.AddTransient<ISuppliersRepository, SuppliersRepository>();
            services.Configure<ExploreNorthwindOptions>(Configuration.GetSection(ExploreNorthwindOptions.ExploreNorthwindOptionsName));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation("Application Startup");
            logger.LogInformation($"App location: {env.ContentRootPath}");
            logger.LogInformation($"Configuration values:\nProductsMaxCount: {Configuration["ExploreNorthwindOptions:ProductsMaxCount"]}");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "image",
                    pattern: "images/{categoryId}",
                    defaults: new { controller = "Categories", action = "Picture" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
