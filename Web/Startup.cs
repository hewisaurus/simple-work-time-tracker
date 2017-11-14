using System;
using Database;
using Database.Dapper;
using Database.Interfaces.Repositories;
using Database.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleWorkTimeTracker.DependencyInjection;
//using SimpleWorkTimeTracker.Data;
using SimpleWorkTimeTracker.Models;
using SimpleWorkTimeTracker.Services;
using StructureMap;

namespace SimpleWorkTimeTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, x =>
                {
                    x.AccessDeniedPath = "/Account/Forbidden";
                    x.LoginPath = "/Account/Login";
                });

            services.AddMvc();

            // Get the connection string from config
            var connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            // Initialise StructureMap
            var container = new Container();
            container.Configure(x =>
            {
                x.AddRegistry(new ServicesRegistry());
                x.AddRegistry(new DbRepositoryRegistry(connectionString));
                x.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
