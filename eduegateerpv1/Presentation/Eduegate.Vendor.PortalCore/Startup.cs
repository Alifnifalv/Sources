using Eduegate.Domain.Entity.IoC.PublicApi;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Globalization;
using WebMarkupMin.AspNetCore3;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using Newtonsoft.Json.Serialization;


namespace Eduegate.Vendor.PortalCore
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

            services.AddWebOptimizer(minifyJavaScript: false, minifyCss: false);

            services.AddSession();

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

            //var environment = new SettingBL(null).GetSettingValue<string>("Environment");
            //var clientName = new SettingBL(null).GetSettingValue<string>("ClientName");
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
                
            app.UseAuthentication();

            app.UseWebMarkupMin();

            app.UseEndpoints(endpoints =>
            {
       
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
