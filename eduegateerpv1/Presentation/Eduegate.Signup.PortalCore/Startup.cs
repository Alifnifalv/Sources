using Eduegate.Domain.Entity.IoC.PublicApi;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.IO;
using WebMarkupMin.AspNetCore3;

namespace Eduegate.Signup.PortalCore
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
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
                     //options.MinificationSettings.AttributeQuotesRemovalMode = WebMarkupMin.Core.HtmlAttributeQuotesRemovalMode.KeepQuotes;
                     //options.MinificationSettings.EmptyTagRenderMode = WebMarkupMin.Core.HtmlEmptyTagRenderMode.SpaceAndSlash;
                     //options.MinificationSettings.MinifyEmbeddedJsonData = false;
                     //options.MinificationSettings.MinifyAngularBindingExpressions = false;
                 })
             .AddHttpCompression();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(options =>
             {
                 options.LoginPath = "/Account/Login/";
                 options.LogoutPath = "/Account/Logout/";
             });


            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
            });

            DbContextService.AddServices(services);

            Environment.SetEnvironmentVariable("dbEduegateERPContext", Configuration.GetConnectionString("dbEduegateERPContext"));
            Environment.SetEnvironmentVariable("dbEduegateSchoolContext", Configuration.GetConnectionString("dbEduegateSchoolContext"));
            Environment.SetEnvironmentVariable("EduegatedERP_LoggerContext", Configuration.GetConnectionString("EduegatedERP_LoggerContext"));
            Environment.SetEnvironmentVariable("dbContentContext", Configuration.GetConnectionString("dbContentContext"));

            var logLevel = Configuration["logLevel"];
            Environment.SetEnvironmentVariable("logLevel", !string.IsNullOrEmpty(logLevel) ? logLevel : "fatel");

            Environment.SetEnvironmentVariable("smtphost", Configuration["smtphost"]);
            Environment.SetEnvironmentVariable("smtpemail", Configuration["smtpemail"]);
            Environment.SetEnvironmentVariable("smtppassword", Configuration["smtppassword"]);
            Environment.SetEnvironmentVariable("fromEmail", Configuration["fromEmail"]);
            Environment.SetEnvironmentVariable("toEmail", Configuration["toEmail"]);
            Environment.SetEnvironmentVariable("port", Configuration["port"]);
            Environment.SetEnvironmentVariable("enablessl", Configuration["enablessl"]);

            Environment.SetEnvironmentVariable("ThumbnailImage", Configuration["ThumbnailImage"]);
            Environment.SetEnvironmentVariable("ListingImage", Configuration["ListingImage"]);
            Environment.SetEnvironmentVariable("SmallImage", Configuration["SmallImage"]);
            Environment.SetEnvironmentVariable("LargeImage", Configuration["LargeImage"]);
            Environment.SetEnvironmentVariable("PageRendererCache", "Global");

            var appId = Configuration["appId"];
            Environment.SetEnvironmentVariable("appId", !string.IsNullOrEmpty(appId) ? appId : "parentportal");

            var culture = Configuration["defaultCultureCode"];
            var cultureInfo = new CultureInfo(culture ?? "en-US");
            var r = new RegionInfo(cultureInfo.Name);

            if (cultureInfo.NumberFormat.CurrencySymbol.Trim().Length > 1)
            {
                cultureInfo.NumberFormat.CurrencySymbol = cultureInfo.NumberFormat.CurrencySymbol + " ";
            }

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
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

            //services.AddWebOptimizer();

            services.AddWebOptimizer(minifyJavaScript: false, minifyCss: false);

            //var environment = new Domain.Setting.SettingBL(null).GetSettingValue<string>("Environment");
            //var clientName = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ClientName");
            //var clientSettingFile = Path.Combine(Environment.CurrentDirectory, "wwwroot\\data\\client-settings\\",
            //    string.Concat("client-settings-", clientName, ".", environment, ".json"));
            //if (File.Exists(clientSettingFile))
            //{
            //    Environment.SetEnvironmentVariable("ClientSettings", System.IO.File.ReadAllText(clientSettingFile));
            //}
            //else
            //{
            //    clientSettingFile = Path.Combine(Environment.CurrentDirectory, "wwwroot\\data\\client-settings\\",
            //            string.Concat("client-settings-", clientName, ".json"));
            //    if (File.Exists(clientSettingFile))
            //    {
            //        Environment.SetEnvironmentVariable("ClientSettings", System.IO.File.ReadAllText(clientSettingFile));
            //    }
            //}
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();
            app.UseDeveloperExceptionPage();

            if (Configuration["allowonlyhttps"] == "true" && Configuration["redirecttowww"] == "true")
            {
                app.UseRewriter(new RewriteOptions()
                 .AddRedirectToHttps()
                 .AddRedirectToWww());
            }
            else
            {
                if (Configuration["allowonlyhttps"] == "true")
                {
                    app.UseRewriter(new RewriteOptions()
                        .AddRedirectToHttps());
                }

                if (Configuration["redirecttowww"] == "true")
                {
                    app.UseRewriter(new RewriteOptions()
                        .AddRedirectToWww());
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/home/error");
                app.UseHsts();
            }

            app.UseWebOptimizer();
            //app.UseHttpsRedirection();
            //app.UseRewriter(new RewriteOptions().AddRedirectToWww());
            app.UseStaticFiles();

            //app.UseXMLSitemap(env.ContentRootPath);
            //app.UseRobotsTxt(env.ContentRootPath);
            app.UseRouting();
            //app.UseHangfireDashboard();
            //backgroundJobs.Enqueue(() => Console.WriteLine("Hello welome to eduegate!"));                      
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWebMarkupMin();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHangfireDashboard();
                //endpoints.MapHub<NotificationHub>("/Hub");
                //endpoints.MapHub<StreamingHub>("/Streaming");

                endpoints.MapControllerRoute(
                    name: "MyArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Use(async (context, next) =>
            {
                var url = context.Request.Path.Value;

                if (context.Request.Path.StartsWithSegments("/robots.txt"))
                {
                    var robotsTxtPath = Path.Combine(env.ContentRootPath, "robots.txt");
                    string output = "User-agent: *  \nDisallow: /";
                    if (File.Exists(robotsTxtPath))
                    {
                        output = await File.ReadAllTextAsync(robotsTxtPath);
                    }

                    output = string.Concat(output, "Sitemap:", context.Request.Host, "sitemap.xml");
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(output);
                }
                else
                {
                    context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = null; // unlimited I guess
                    await next.Invoke();
                }
            });
        }

    }
}