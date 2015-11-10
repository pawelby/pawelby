using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Newtonsoft.Json.Serialization;
using Pawelby.Models;
using Pawelby.Services;
using Pawelby.ViewModels;

namespace Pawelby
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
#if !DEBUG
                config.Filters.Add(new RequireHttpsAttribute());
#endif
            })
                .AddJsonOptions(
                    opt => { opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); });

            services.AddIdentity<PawelbyUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<PawelbyContext>();

            services.ConfigureCookieAuthentication(config =>
            {
                config.LoginPath = "/Auth/Login";
                config.Notifications = new CookieAuthenticationNotifications()
                {
                    OnApplyRedirect = ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") &&
                            ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                    }
                };

            });

            services.AddLogging();

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<PawelbyContext>();

            services.AddScoped<CoordService>();
            services.AddTransient<PawelbyContextSeedData>();
            services.AddScoped<IPawelbyRepository, PawelbyRepository>();


#if DEBUG
            services.AddScoped<IMailService, DebugMailService>();
#endif

        }

        public async void Configure(IApplicationBuilder app, 
            PawelbyContextSeedData seeder, 
            ILoggerFactory loggerFactory,
            IHostingEnvironment env)
        {
            loggerFactory.AddDebug(LogLevel.Information);

            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug(LogLevel.Information);
                app.UseErrorPage();
            }
            else
            {
                loggerFactory.AddDebug(LogLevel.Debug);
            }

            app.UseStaticFiles();

            app.UseIdentity();

            Mapper.Initialize(config =>
            {
                config.CreateMap<Trip, TripViewModel>().ReverseMap();
                config.CreateMap<Stop, StopViewModel>().ReverseMap();
            });

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                    );
            });

            await seeder.EnsureSeedDataAsync();
        }
    }
}
