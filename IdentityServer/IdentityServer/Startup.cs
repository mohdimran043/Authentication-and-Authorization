﻿using IdentityServer.IdentityServerExtensions;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MOI.Patrol.ORM_Auth;
using Microsoft.Extensions.Configuration;
using IdentityServer4.Services;
using IdentityServer.Initialize;

namespace IdentityServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json");
            Configuration = configurationBuilder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<MOI_ApplicationPermissionContext>(options =>
            {
                options.UseNpgsql(Configuration["ConnectionStrings:AuthenticationConnection"]);
            });

            services.AddMvc();
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>()
                  .AddTransient<IProfileService, ProfileService>();
            services.AddIdentityServer().AddDeveloperSigningCredential()
             //.AddConfigurationStore(option => option.ConfigureDbContext = builder => builder.UseNpgsql(Configuration.GetConnectionString("AuthenticationConnection"), options =>
             //    options.MigrationsAssembly("IdentityServer")))
             //    .AddOperationalStore(option =>
             //           option.ConfigureDbContext = builder => builder.UseNpgsql(Configuration.GetConnectionString("AuthenticationConnection"), options =>
             //           options.MigrationsAssembly("IdentityServer")));
             .AddInMemoryClients(Config.GetClients())
             .AddInMemoryApiResources(Config.GetApiResources())
             .AddInMemoryIdentityResources(Config.GetIdentityResources());


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, MOI_ApplicationPermissionContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            // DatabaseInitializer.Initialize(app, context);
        }
    }
}
