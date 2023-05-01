using BBMS.Data;
using BBMS.Helpers;
using BBMS.Models;
using BBMS.Repository;
using BBMS.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBMS
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
            services.AddDbContext<BloodBankDbContext>(
                option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionStrings"))
                );
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BloodBankDbContext>().AddDefaultTokenProviders();
            services.AddMvc();
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IBloodGroupRepository, BloodGroupRepository>();
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();
            services.AddScoped<IUserService, UserService>();
            services.Configure<SMTPConfigModel>(Configuration.GetSection("SMTPConfig"));
            services.AddScoped<IEmailService, EmailService>();

            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequiredLength = 5;
                option.Password.RequireDigit = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredUniqueChars = 1;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;

                option.SignIn.RequireConfirmedEmail = true;


            });
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
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                  endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
            });
        }
    }
}
