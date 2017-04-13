using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Weeblo.Services;
using Microsoft.Extensions.Configuration;
using Weeblo.Models;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using Weeblo.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Weeblo
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;


        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath) //Sets base path based on server root file
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            _config = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940

        // 500 Error - Server Error, unable to find required services
        // 404 Error - Unable to find the right pages

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc(config =>
            {
                if (_env.IsProduction())
                {
                    config.Filters.Add(new RequireHttpsAttribute());  //For greater security
                }
            }) //Mvc requires dependency injections/services/interfaces to be configured
            .AddJsonOptions(config =>
            {
                config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); //Changes Json results to CamelCase
            });

            services.AddIdentity<ProjectUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
                //config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
                config.Cookies.ApplicationCookie.LoginPath = new PathString("/auth/login");
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = async ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") &&
                        ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }
                        else
                        {
                        ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        await Task.Yield();
                    }
                };
            })
            .AddEntityFrameworkStores<ProjectContext>();

            //AddSingleton creates a single instance of an object and ensure there is only ever one instance of it as well as providing centralised access to the single instance
            services.AddSingleton(_config);

            // Three tests for IHostingEnvironment - IsStaging (Pre-Production, final test before deployment), IsProduction (App is running live, configuret to maximize security and performance), IsDevelopment (Developing App)
            // Or "||"
            // Go to solution properties to set environment setting

            if (_env.IsEnvironment("Development") || _env.IsEnvironment("Testing"))
            {
                services.AddScoped<IMailService, DebugMailService>(); //Transient means it will create an instantce when needed
                                                                      //AddScoped creates an instance for each request
                                                                      //AddSingleton creates one instance when needed and passed over and over again
            }
            else
            {
                // Implement a real mail service
            }

            services.AddDbContext<ProjectContext>(); //Configures database context, must add using Weeblo.Models

            services.AddTransient<ProjectContextSeedData>();

            services.AddScoped<IProjectRepository, ProjectRepository>(); //Repositories

            services.AddLogging(); //Logging
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ProjectContextSeedData seeder)
        {

            //Order matters for these config files

            //Mapper configuration to store items back to the database
            Mapper.Initialize(config =>
            {
                config.CreateMap<ProjectViewModel, Project>().ReverseMap();  //Match field names to and from, add reverse map at the end so that it goes both directions
                config.CreateMap<StoryViewModel, Story>().ReverseMap();
            });

            //app.UseDefaultFiles(); //allows you to connect to wwwroot files such as index.html by default
            // must put DefaultFiles before static files (i.e. order of these middleware pieces is important
            app.UseStaticFiles(); //allows you to access files at wwwroot

            //Order matters for these config files
            app.UseIdentity();

            //Need to tell server what routes belong to what controllers in MVC
            //Need to include name of route, template, and defaults if not specified
            //Curly braces {} in template signify a pattern to be used
            //Use question mark in route to signify that is is optional
            //Defaults specify what to use for controller and action if nothing provided, must create a "new"
            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                    );
            }
                );

            loggerFactory.AddConsole();

            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                loggerFactory.AddDebug(LogLevel.Error);
            }


            seeder.EnsureSeedData().Wait();

        }
    }
}
