using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using XKCDDemo.Repository.Implementations;
using XKCDDemo.Repository.Interfaces;

namespace XKCDDemo.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //If more clients are added, the strategy of dependencies should change to named dependencies
            services.AddHttpClient<IXKCDApi, XKCDApiHelper>();
            #region setup of custom services and repositories dependencies
            //"https://xkcd.com/"
            //For getting all the service layer types using a example type as reference of the assembly and all the persistence dependencies
            var dependencyAssemblies = new[] {
                Assembly.Load("XKCDDemo.Service"),
                Assembly.Load("XKCDDemo.Repository")
            };
            foreach (var assembly in dependencyAssemblies)
            {
                var interfaces = assembly.GetTypes()
                .Where(type => type.IsInterface && type != typeof(IXKCDApi))
                .ToList();
                var implementations = assembly.GetTypes()
                        .Where(type => type.IsClass && interfaces.Any(si => si.IsAssignableFrom(type)))
                        .ToList();
                foreach (Type currentInterface in interfaces)
                {
                    //Fetch all the implementations in assembly
                    var currentInterfaceImplementations = implementations
                        .Where(impl => currentInterface.IsAssignableFrom(impl))
                        .ToList();
                    if (currentInterfaceImplementations.Any())
                    {
                        foreach (Type specificImplementation in currentInterfaceImplementations)
                        {
                            services.AddScoped(currentInterface, specificImplementation);
                        }
                    }
                }

            }
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
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
        }
    }
}
