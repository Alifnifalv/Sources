using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection.Extensions;
//using Mjml.AspNetCore;
using Eduegate.Domain.Entity.IoC.PublicApi;
using Eduegate.Hub.Client;
using WebMarkupMin.AspNetCore3;
using Eduegate.Hub;
using Microsoft.AspNetCore.Authorization;
using OfficeOpenXml;
using DinkToPdf.Contracts;
using DinkToPdf;
using Eduegate.ERP.Admin.Helpers;

namespace Eduegate.ERP.AdminCore
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation(); // Enable runtime compilation for Razor views
            services.AddSignalR();
            
            services.AddSingleton<NotificationClient>();
            services.AddSingleton<ChatClient>();
            services.AddSingleton<RealtimeClient>();

            services.AddHttpClient();

            // Set the license context for EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Load the WkHtmlToPdf library
            WkHtmlToPdfLoader.LoadLibrary();

            // Add DinkToPdf services
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
            });
            services.AddWebMarkupMin(
             options =>
             {
                 options.AllowMinificationInDevelopmentEnvironment = true;
                 options.AllowCompressionInDevelopmentEnvironment = true;
             })
             .AddHtmlMinification(
                 options =>
                 {
                     //options.MinificationSettings.RemoveRedundantAttributes = true;
                     //options.MinificationSettings.RemoveHttpProtocolFromAttributes = true;
                     //options.MinificationSettings.RemoveHttpsProtocolFromAttributes = true;
                 })
             .AddHttpCompression();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
            {
                x.LoginPath = "/Account/Login";
                //x.AccessDeniedPath = "/Account/AccessDenied";
                x.SlidingExpiration = true;
            });

            //services.AddMjmlServices(o =>
            //{
            //    o.DefaultKeepComments = true;
            //    o.DefaultBeautify = true;
            //});

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            Environment.SetEnvironmentVariable("dbEduegateERPContext", Configuration.GetConnectionString("dbEduegateERPContext"));
            Environment.SetEnvironmentVariable("dbEduegateSchoolContext", Configuration.GetConnectionString("dbEduegateSchoolContext"));
            Environment.SetEnvironmentVariable("dbContentContext", Configuration.GetConnectionString("dbContentContext"));
            Environment.SetEnvironmentVariable("EduegatedERP_LoggerContext", Configuration.GetConnectionString("EduegatedERP_LoggerContext"));

            DbContextService.AddServices(services);
            //Domain.Repository.ShoppingCarts.Repository.Register(services);

            var appSettings = Configuration.GetSection("AppSettings");
            Environment.SetEnvironmentVariable("smtphost", appSettings["smtphost"]);
            Environment.SetEnvironmentVariable("smtpemail", appSettings["smtpemail"]);
            Environment.SetEnvironmentVariable("smtppassword", appSettings["smtppassword"]);
            Environment.SetEnvironmentVariable("fromEmail", appSettings["fromEmail"]);
            Environment.SetEnvironmentVariable("toEmail", appSettings["toEmail"]);
            Environment.SetEnvironmentVariable("port", appSettings["port"]);
            Environment.SetEnvironmentVariable("enablessl", appSettings["enablessl"]);
            Environment.SetEnvironmentVariable("enableNoficationHub", appSettings["enableNoficationHub"]);

            var appId = appSettings["appId"];
            Environment.SetEnvironmentVariable("appId", !string.IsNullOrEmpty(appId) ? appId : "erpportal");

            services.AddHangfire(configuration => configuration
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("dbEduegateQueueContext"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));
            services.AddHangfireServer();
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });
            services
                .AddWebMarkupMin(
             options =>
             {
                 options.AllowMinificationInDevelopmentEnvironment = false;
                 options.AllowCompressionInDevelopmentEnvironment = false;
             })
             .AddHtmlMinification(
                 options =>
                 {
                     //options.MinificationSettings.RemoveRedundantAttributes = true;
                     //options.MinificationSettings.RemoveHttpProtocolFromAttributes = true;
                     //options.MinificationSettings.RemoveHttpsProtocolFromAttributes = true;
                 })
             .AddHttpCompression();
            services.AddWebOptimizer();
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
            }

            app.Use(async (context, next) =>
            {
                context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = null; // unlimited I guess
                await next.Invoke();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseHangfireDashboard();
            //app.UseCors("CORSPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseWebOptimizer();
            app.UseResponseCompression();
            app.UseWebMarkupMin();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHangfireDashboard();

                if (Environment.GetEnvironmentVariable("enableNoficationHub") != null &&
                    Environment.GetEnvironmentVariable("enableNoficationHub") == "true")
                {
                    endpoints.MapHub<NotificationHub>("/NotificationHub");
                }

                endpoints.MapControllerRoute(
                 name: "MyArea",
                 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
