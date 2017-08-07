using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;

namespace Scout.Auth
{
    public class Startup
    {
        IHostingEnvironment _hostingEnv;

        public Startup(IHostingEnvironment env)
        {
            _hostingEnv = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<MvcOptions>(options =>
            {
                // Set LocalTest:skipSSL to true to skip SSL requirement in 
                // debug mode. This is useful when not using Visual Studio.
                if (!_hostingEnv.IsDevelopment())
                {
                    options.Filters.Add(new RequireHttpsAttribute());
                }
            });

            // Enable cross site browsing.
            services.AddCors();

            // Add framework services.
            services.AddMvc();
            //services.AddMvc(config =>
            //{
            //    // Make authentication compulsory across the board (i.e. shut down EVERYTHING unless explicitly opened up).
            //    var policy = new AuthorizationPolicyBuilder()
            //                     .RequireAuthenticatedUser()
            //                     .Build();
            //    config.Filters.Add(new AuthorizeFilter(policy));
            //});

            Register(services);

            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Shows UseCors with CorsPolicyBuilder.
            //app.UseCors(builder =>
            //   builder.WithOrigins("http://scouts"));
            app.UseCors(builder =>
           builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()
            );

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            Mapper.Initialize(cfg => {
                cfg.CreateMissingTypeMaps = true;
            });

            Mapper.AssertConfigurationIsValid();
        }

        private void Register(IServiceCollection services)
        {

            // Create the container builder.
            var builder = new ContainerBuilder();

            // Register dependencies, populate the services from
            // the collection, and build the container. If you want
            // to dispose of the container at the end of the app,
            // be sure to keep a reference to it as a property or field.
            //builder.RegisterModule(new Data.RegistrationModule());
            builder.RegisterModule(new StartupLogic.RegistrationModule(Configuration));
            builder.Populate(services);
            this.ApplicationContainer = builder.Build();
        }
    }
}
