using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// database
using Microsoft.EntityFrameworkCore;
using diary.Data;
using diary.Models;
using Pomelo;

// Identity
using Microsoft.AspNetCore.Identity;

// Kendo UI
using Newtonsoft.Json.Serialization;

namespace diary
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
                  services
                        .AddMvc()
                        .AddJsonOptions(
                              options =>
                                    options.SerializerSettings.ContractResolver = new DefaultContractResolver());

                  services.AddKendo();

                  services
                        .AddDbContext<diaryContext>(
                              options =>
                                    options.UseMySql(Configuration.GetConnectionString("MySqlConnections")));

                  // Identity Services
                  services.AddIdentity<User, IdentityRole>()
                        .AddEntityFrameworkStores<diaryContext>()
                        .AddDefaultTokenProviders();

                  services.Configure<IdentityOptions>(options =>
                  {
                        // Password settings
                        options.Password.RequireDigit = true;
                        options.Password.RequiredLength = 8;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireLowercase = false;
                        // options.Password.RequiredUniqueChars = 6;

                        // Lockout settings
                        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                        options.Lockout.MaxFailedAccessAttempts = 10;
                        options.Lockout.AllowedForNewUsers = true;

                        // User settings
                        options.User.RequireUniqueEmail = true;
                  });

                  services.ConfigureApplicationCookie(options =>
                  {
                        // Cookie settings
                        options.Cookie.HttpOnly = true;
                        options.Cookie.Expiration = TimeSpan.FromDays(150);

                        // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                        options.LoginPath = "/Account/Index";

                        // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                        options.LogoutPath = "/Account/Logout";

                        // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                        options.AccessDeniedPath = "/Account/AccessDenied";
                        options.SlidingExpiration = true;
                  });
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
                        app.UseExceptionHandler("/Home/Error");
                  }

                  app.UseKendo(env);
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
