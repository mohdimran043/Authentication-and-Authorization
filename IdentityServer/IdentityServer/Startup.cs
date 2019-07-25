using IdentityServer.IdentityServerExtensions;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MOI.Patrol.ORM_Auth;
using Microsoft.Extensions.Configuration;
using IdentityServer4.Services;
using IdentityServer.Initialize;
using MOI.IdentityServer.DataAccess.DbContexts;
using Microsoft.Extensions.Logging;
using MOI.IdentityServer.Helpers;
using MOI.IdentityServer.IdentityServerExtensions;
using MOI.IdentityServer.Repository;

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
            }).AddEntityFrameworkNpgsql().AddDbContext<AuthDbContext>(options =>
            {
                options.UseNpgsql(Configuration["ConnectionStrings:IdentityAuthDB"]);
            });

            services.AddMvc();

            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>().AddTransient<IProfileService, ProfileService>();


            //services.AddIdentityServer().AddDeveloperSigningCredential()
            // .AddInMemoryClients(Config.GetClients())
            // .AddInMemoryApiResources(Config.GetApiResources())
            // .AddInMemoryIdentityResources(Config.GetIdentityResources());


            services.AddIdentityServer().AddDeveloperSigningCredential()
             .AddConfigurationStore(option => option.ConfigureDbContext = builder => builder.UseNpgsql(Configuration.GetConnectionString("IdentityAuthDB"), options =>
                 options.MigrationsAssembly("MOI.IdentityServer.DataAccess")))
                 .AddOperationalStore(option =>
                        option.ConfigureDbContext = builder => builder.UseNpgsql(Configuration.GetConnectionString("IdentityAuthDB"), options =>
                        options.MigrationsAssembly("MOI.IdentityServer.DataAccess")));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AuthDbContext context, ILoggerFactory loggerFactory)
        {
            Utilities.ConfigureLogger(loggerFactory);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            DatabaseInitializer.Initialize(app, context);
            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();


        }
    }
}
