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
//using SimpleWorkTimeTracker.Data;
using SimpleWorkTimeTracker.Models;
using SimpleWorkTimeTracker.Services;

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
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(opt => opt.LoginPath = "/Account/Login");
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            // Auth
            services.AddTransient<IAuthentication, Authentication>();
            // Database interfaces
            //For<IConnectionFactory>()
            //    .Use<MySqlConnectionFactory>()
            //    .Ctor<string>("connectionString")
            //    .Is(repositoryConnectionString);
            var connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddSingleton<IConnectionFactory, MysqlConnectionFactory>(p => new MysqlConnectionFactory(connectionString));
            services.AddSingleton<IAuthenticationQueryRepository, AuthenticationQueryRepository>();

            services.AddMvc();
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
