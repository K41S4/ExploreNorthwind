using ExploreNorthwind.ConfigurationOptions;
using ExploreNorthwind.Middlewares;
using ExploreNorthwind.Middlewares.Helpers;
using ExploreNorthwindDataAccess.Models;
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
using Microsoft.OpenApi.Models;
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

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExploreNorthwindAPI", Version = "v1" }));

            var exploreNorthwindOptions = new ExploreNorthwindOptions();
            Configuration.GetSection(ExploreNorthwindOptions.ExploreNorthwindOptionsName).Bind(exploreNorthwindOptions);
            
            services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(exploreNorthwindOptions.NorthwindConnectionString));
            services.AddTransient<INorthwindContext, NorthwindContext>();
            services.AddTransient<ICategoriesRepository, CategoriesRepository>();
            services.AddTransient<IProductsRepository, ProductsRepository>();
            services.AddTransient<ISuppliersRepository, SuppliersRepository>();
            services.AddTransient<IDataOperationsHelper, DataOperationsHelper>();
            services.Configure<ExploreNorthwindOptions>(Configuration.GetSection(ExploreNorthwindOptions.ExploreNorthwindOptionsName));

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<NorthwindContext>();


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

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExploreNorthwindAPI v1"));

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseImageCaching();

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
