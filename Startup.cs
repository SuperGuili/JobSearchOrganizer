using ChustaSoft.Tools.SecureConfig;
using JobSearchOrganizer.Models;
using JobSearchOrganizer.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer
{
    public class Startup
    {
        private IConfiguration _config;
        //private readonly AppSettings appSettings;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.SetUpSecureConfig<AppSettings>(_config, _config["SECRET_KEY"]);

            services.AddSingleton<DataProtectionPurposeStrings>();

            services.AddDbContextPool<AppDbContext>(options =>
            {
                //options.UseSqlServer(_config.GetConnectionString("JobDbConnection"));
                var ConnectString = services.SetUpSecureConfig<AppSettings>(_config, _config["SECRET_KEY"]).ConnectionStrings.Values.First();

                options.UseSqlServer(ConnectString);
            });

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));

            }).AddRazorRuntimeCompilation()
              .AddXmlSerializerFormatters();

            services.AddAuthentication()
                .AddGoogle(options =>
            {
                var clientId = services.SetUpSecureConfig<AppSettings>(_config, _config["SECRET_KEY"]).GoogleClientId;
                var clientSecret = services.SetUpSecureConfig<AppSettings>(_config, _config["SECRET_KEY"]).GoogleClientSecret;

                options.ClientId = clientId;
                options.ClientSecret = clientSecret;
            }).AddFacebook(options =>
            {
                var facebookAppId = services.SetUpSecureConfig<AppSettings>(_config, _config["SECRET_KEY"]).FacebookAppId;
                var facebookAppSecret = services.SetUpSecureConfig<AppSettings>(_config, _config["SECRET_KEY"]).FacebookAppSecret;

                options.AppId = facebookAppId;
                options.AppSecret = facebookAppSecret;

            }).AddMicrosoftAccount(options =>
            {
                var MicrosoftClientId = services.SetUpSecureConfig<AppSettings>(_config, _config["SECRET_KEY"]).MicrosoftClientId;
                var MicrosoftClientSecret = services.SetUpSecureConfig<AppSettings>(_config, _config["SECRET_KEY"]).MicrosoftClientSecret;

                options.ClientId = MicrosoftClientId;
                options.ClientSecret = MicrosoftClientSecret;
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                options.SignIn.RequireConfirmedEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

                options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";

            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<CustomEmailConfirmationTokenProvider<ApplicationUser>>("CustomEmailConfirmation");

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                policy => policy.RequireClaim("Delete Role", "true"));

            });

            //services.Configure<IISServerOptions>(options =>
            //{
            //    options.AutomaticAuthentication = false;
            //});

            //Change the life span of all the tokens types
            //services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromHours(5));
            services.Configure<CustomEmailConfirmationTokenProviderOptions>(o => o.TokenLifespan =
            TimeSpan.FromDays(3));

            services.AddScoped<IJobRepository, SQLJobRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }


            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();
            app.UseAuthentication();

            app.UseMvc(routes =>
           {
               routes.MapRoute("default", "{controller=Home}/{action=Welcome}/{id?}");
           });

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
