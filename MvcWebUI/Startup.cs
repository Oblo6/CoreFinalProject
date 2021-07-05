using AppCore.DataAccess.Configs;
using AppCore.Utils;
using AppCore.Utils.Bases;
using Business.Services;
using DataAccess.EntityFramework.Contexts;
using DataAccess.EntityFramework.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcWebUI.Settings;
using System;

namespace MvcWebUI
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

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(config =>
                {
                    config.LoginPath = "/Accounts/Login";
                    config.AccessDeniedPath = "/Accounts/AccessDenied";
                    config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    config.SlidingExpiration = true;
                });

            services.AddSession(config =>
            {
                config.IdleTimeout = TimeSpan.FromMinutes(30);
            });


            ConnectionConfig.ConnectionString = Configuration.GetConnectionString("CoalContext");

            services.AddScoped<DbContext, CoalContext>();
            services.AddScoped<ExpenseRepositoryBase, ExpenseRepository>();
            services.AddScoped<UserRepositoryBase, UserRepository>();
            services.AddScoped<CountryRepositoryBase, CountryRepository>();
            services.AddScoped<CityRepositoryBase, CityRepository>();
            services.AddScoped<RoleRepositoryBase, RoleRepository>();
            services.AddScoped<CollectiveRepositoryBase, CollectiveRepository>();
            services.AddScoped<CollectiveUserRepositoryBase, CollectiveUserRepository>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICollectiveUserService, CollectiveUserService>();
            services.AddScoped<ICollectiveService, CollectiveService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAccountService, AccountService>();

            AppSettingsUtilBase appSettingsUtil = new AppSettingsUtil(Configuration);
            appSettingsUtil.Bind<AppSettings>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
