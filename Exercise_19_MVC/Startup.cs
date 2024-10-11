using Exercise_21.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Exercise_21
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PhoneBookContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("PhoneBookContext")));
            services.AddTransient<INoteData, NoteData>();
            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<PhoneBookContext>()
                .AddDefaultTokenProviders();
            services.AddAuthorization(
                
            );

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6; // минимальное количество знаков в пароле
                options.Lockout.MaxFailedAccessAttempts = 10; // количество попыток о блокировки
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.AllowedForNewUsers = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // конфигурация Cookie с целью использования их для хранения авторизации
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.SlidingExpiration = false;
          
            });

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();         
            app.UseMvc(routes =>
                routes.MapRoute(
                name: "default",
                template: "{controller=Note}/{action=Index}/{id?}"));
        }
    }
}
