using Eduegate.Domain;
using Eduegate.Domain.Entity.IoC.PublicApi;
using Eduegate.Domain.Setting;
using Hangfire;
using Hangfire.SqlServer;
//using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
//using Mjml.AspNetCore;
//using Eduegate.Domain.Entity.IoC.PublicApi;
//using Eduegate.Hub.Client;
using System;
using System.Globalization;
using System.IO;
using WebMarkupMin.AspNetCore3;
//using WebMarkupMin.AspNetCore3;
//using Eduegate.Hub;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.School.Models.School;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Eduegate.OnlineExam.PortalCore
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
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

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
                //options.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ss.sssZ";
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(options =>
             {
                 options.LoginPath = "/Account/Login/";
                 options.LogoutPath = "/Account/Logout/";
             });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    });

            //services.AddWebOptimizer();
            services.AddWebOptimizer(minifyJavaScript: false, minifyCss: false);

            services.AddSession();

            //services.AddControllersWithViews().AddDataAnnotationsLocalization(options =>
            //{
            //    options.DataAnnotationLocalizerProvider = (type, factory) =>
            //        factory.Create(typeof(SharedResource));
            //});

            //services.AddAuthentication()
            //     .AddOAuth("TikTok", options =>
            //     {
            //         options.ClientId = Configuration["Authentication:Tiktok:ClientId"]; ; // Replace with your TikTok Client ID
            //         options.ClientSecret = Configuration["Authentication:Tiktok:ClientSecret"]; // Replace with your TikTok Client Secret
            //         options.CallbackPath = new PathString("/signin-tiktok"); // Replace with your callback path
            //         options.AuthorizationEndpoint = "https://open-api.tiktok.com/platform/oauth/connect";
            //         options.TokenEndpoint = "https://open-api.tiktok.com/platform/oauth/token";
            //         options.UserInformationEndpoint = "https://open-api.tiktok.com/platform/oauth/user";
            //     })
            //     .AddMicrosoftAccount(microsoftOptions =>
            //     {
            //         microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ClientId"];
            //         microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
            //     })
            //     .AddGoogle(googleOptions =>
            //     {
            //         googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
            //         googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //     })
            //     .AddTwitter(twitterOptions =>
            //     {
            //         twitterOptions.ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"];
            //         twitterOptions.ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"];
            //     })
            //     .AddFacebook(facebookOptions =>
            //     {
            //         facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
            //         facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //     })
            //     .AddOAuth("LinkedIn", options =>
            //     {
            //         options.ClientId = Configuration["Authentication:LinkedInApi:ClientId"];
            //         options.ClientSecret = Configuration["Authentication:LinkedInApi:ClientSecret"];
            //         options.CallbackPath = "/SocialMedia/LinkedInCallback";
            //         options.AuthorizationEndpoint = "https://www.linkedin.com/oauth/v2/authorization";
            //         options.TokenEndpoint = "https://www.linkedin.com/oauth/v2/accessToken";
            //         options.UserInformationEndpoint = "https://api.linkedin.com/v2/me";
            //         options.SaveTokens = true;

            //         options.Events = new OAuthEvents
            //         {
            //             OnCreatingTicket = context =>
            //             {
            //                 // Retrieve the user's profile URL
            //                 string profileUrl = context.User.ValueKind.ToString();
            //                 // Save the profile URL in claims
            //                 context.Identity.AddClaim(new System.Security.Claims.Claim("LinkedInProfileUrl", profileUrl));

            //                 return Task.CompletedTask;
            //             }
            //         };
            //     });

            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
            });

            DbContextService.AddServices(services);
            //Domain.Repository.ShoppingCarts.Repository.Register(services);
            Environment.SetEnvironmentVariable("dbEduegateERPContext", Configuration.GetConnectionString("dbEduegateERPContext"));
            Environment.SetEnvironmentVariable("dbEduegateSchoolContext", Configuration.GetConnectionString("dbEduegateSchoolContext"));
            Environment.SetEnvironmentVariable("EduegatedERP_LoggerContext", Configuration.GetConnectionString("EduegatedERP_LoggerContext"));
            Environment.SetEnvironmentVariable("dbContentContext", Configuration.GetConnectionString("dbContentContext"));

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

            var culture = Configuration["defaultCultureCode"];
            var cultureInfo = new CultureInfo(culture ?? "en-US");
            var r = new RegionInfo(cultureInfo.Name);

            if (cultureInfo.NumberFormat.CurrencySymbol.Trim().Length > 1)
            {
                cultureInfo.NumberFormat.CurrencySymbol = cultureInfo.NumberFormat.CurrencySymbol + " ";
            }

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            //services.AddHangfire(configuration => configuration
            //    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //    .UseSimpleAssemblyNameTypeSerializer()
            //    .UseRecommendedSerializerSettings()
            //    .UseSqlServerStorage(Configuration.GetConnectionString("dbSkienQueueContext"), new SqlServerStorageOptions
            //    {
            //        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            //        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            //        QueuePollInterval = TimeSpan.Zero,
            //        UseRecommendedIsolationLevel = true,
            //        DisableGlobalLocks = true
            //    }));
            //services.AddHangfireServer();

            var environment = new SettingBL(null).GetSettingValue<string>("Environment");
            var clientName = new SettingBL(null).GetSettingValue<string>("ClientName");
            var clientSettingFile = Path.Combine(Environment.CurrentDirectory, "wwwroot\\data\\client-settings\\",
                string.Concat("client-settings-", clientName, ".", environment, ".json"));
            if (File.Exists(clientSettingFile))
            {
                Environment.SetEnvironmentVariable("ClientSettings", System.IO.File.ReadAllText(clientSettingFile));
            }
            else
            {
                clientSettingFile = Path.Combine(Environment.CurrentDirectory, "wwwroot\\data\\client-settings\\",
                        string.Concat("client-settings-", clientName, ".json"));
                if (File.Exists(clientSettingFile))
                {
                    Environment.SetEnvironmentVariable("ClientSettings", System.IO.File.ReadAllText(clientSettingFile));
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();

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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseSession();

            app.UseWebOptimizer();
            //app.UseRewriter(new RewriteOptions().AddRedirectToWww());

            //app.UseXMLSitemap(env.ContentRootPath);
            //app.UseRobotsTxt(env.ContentRootPath);
            //app.UseHangfireDashboard();
            //backgroundJobs.Enqueue(() => Console.WriteLine("Hello welome to skien!"));                      
            app.UseAuthentication();

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
