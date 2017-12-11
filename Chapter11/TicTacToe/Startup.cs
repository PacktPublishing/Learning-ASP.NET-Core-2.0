using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Services;
using TicTacToe.Extensions;
using Microsoft.AspNetCore.Routing;
using TicTacToe.Models;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using TicTacToe.Options;
using TicTacToe.Filters;
using TicTacToe.ViewEngines;
using Microsoft.AspNetCore.Mvc.Formatters;
using Halcyon.Web.HAL.Json;
using TicTacToe.Data;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using TicTacToe.Monitoring;
using System.Diagnostics;

namespace TicTacToe
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _hostingEnvironment { get; }
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public void ConfigureCommonServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Localization");
            services.AddMvc(o =>
            {
                o.Filters.Add(typeof(DetectMobileFilter));

                o.OutputFormatters.RemoveType<JsonOutputFormatter>();
                o.OutputFormatters.Add(new JsonHalOutputFormatter(new string[] { "application/hal+json", "application/vnd.example.hal+json", "application/vnd.example.hal.v1+json" }));
            }).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, options => options.ResourcesPath = "Localization").AddDataAnnotationsLocalization();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorAccessLevelPolicy", policy => policy.RequireClaim("AccessLevel", "Administrator"));
            });

            services.AddTransient<ApplicationUserManager>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGameInvitationService, GameInvitationService>();
            services.AddScoped<IGameSessionService, GameSessionService>();            

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<GameDbContext>((serviceProvider, options) =>
                    options.UseSqlServer(connectionString)
                            .UseInternalServiceProvider(serviceProvider)
                            );

            services.AddScoped(typeof(DbContextOptions<GameDbContext>), (serviceProvider) =>
            {
                return new DbContextOptionsBuilder<GameDbContext>()
                    .UseSqlServer(connectionString).Options;
            });

            services.Configure<EmailServiceOptions>(_configuration.GetSection("Email"));
            services.AddEmailService(_hostingEnvironment, _configuration);
            services.AddTransient<IEmailTemplateRenderService, EmailTemplateRenderService>();
            services.AddTransient<EmailViewEngine, EmailViewEngine>();

            services.AddRouting();
            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddIdentity<UserModel, RoleModel>(options =>
            {
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<GameDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(options => {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie().AddFacebook(facebook =>
            {
                facebook.AppId = "123";
                facebook.AppSecret = "123";
                facebook.ClientId = "123";
                facebook.ClientSecret = "123";
            });

            services.AddApplicationInsightsTelemetry(_configuration);
            var section = _configuration.GetSection("Monitoring");
            var monitoringOptions = new MonitoringOptions();
            section.Bind(monitoringOptions);
            services.AddSingleton(monitoringOptions);

            if (monitoringOptions.MonitoringType == "azureapplicationinsights")
            {
                services.AddSingleton<IMonitoringService, AzureApplicationInsightsMonitoringService>();
            }
            else if (monitoringOptions.MonitoringType == "amazonwebservicescloudwatch")
            {
                services.AddSingleton<IMonitoringService, AmazonWebServicesMonitoringService>();
            }
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureCommonServices(services);
        }

        public void ConfigureStagingServices(IServiceCollection services)
        {
            ConfigureCommonServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureCommonServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DiagnosticListener diagnosticListener)
        {
            var listener = new ApplicationDiagnosticListener();
            diagnosticListener.SubscribeWithAdapter(listener);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();

            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapGet("CreateUser", context =>
            {
                var firstName = context.Request.Query["firstName"];
                var lastName = context.Request.Query["lastName"];
                var email = context.Request.Query["email"];
                var password = context.Request.Query["password"];
                var userService = context.RequestServices.GetService<IUserService>();
                userService.RegisterUser(new UserModel { FirstName = firstName, LastName = lastName, Email = email, Password = password });
                return context.Response.WriteAsync($"User {firstName} {lastName} has been sucessfully created.");
            });
            var newUserRoutes = routeBuilder.Build();
            app.UseRouter(newUserRoutes);

            app.UseWebSockets();
            app.UseCommunicationMiddleware();

            var supportedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            localizationOptions.RequestCultureProviders.Clear();
            localizationOptions.RequestCultureProviders.Add(new CultureProviderResolverService());

            app.UseRequestLocalization(localizationOptions);

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "areaRoute",
                        template: "{area:exists}/{controller=Home}/{action=Index}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseStatusCodePages("text/plain", "HTTP Error - Status Code: {0}");

            var provider = app.ApplicationServices;
            var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            using (var context = scope.ServiceProvider.GetRequiredService<GameDbContext>())
            {
                context.Database.Migrate();
            }
        }
    }
}
