using Eduegate.Domain.Entity.IoC.PublicApi;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddWebMarkupMin(options =>
{
    options.AllowMinificationInDevelopmentEnvironment = true;
    options.AllowCompressionInDevelopmentEnvironment = true;
})
.AddHtmlMinification()
.AddHttpCompression();

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddMvc()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login/";
        options.LogoutPath = "/Account/Logout/";
    });

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueCountLimit = int.MaxValue;
});

DbContextService.AddServices(builder.Services);

var configuration = builder.Configuration;

// Set environment variables
Environment.SetEnvironmentVariable("dbEduegateERPContext", configuration.GetConnectionString("dbEduegateERPContext"));
Environment.SetEnvironmentVariable("dbEduegateSchoolContext", configuration.GetConnectionString("dbEduegateSchoolContext"));
Environment.SetEnvironmentVariable("EduegatedERP_LoggerContext", configuration.GetConnectionString("EduegatedERP_LoggerContext"));
Environment.SetEnvironmentVariable("dbContentContext", configuration.GetConnectionString("dbContentContext"));

var logLevel = configuration["logLevel"];
Environment.SetEnvironmentVariable("logLevel", !string.IsNullOrEmpty(logLevel) ? logLevel : "fatal");

Environment.SetEnvironmentVariable("smtphost", configuration["smtphost"]);
Environment.SetEnvironmentVariable("smtpemail", configuration["smtpemail"]);
Environment.SetEnvironmentVariable("smtppassword", configuration["smtppassword"]);
Environment.SetEnvironmentVariable("fromEmail", configuration["fromEmail"]);
Environment.SetEnvironmentVariable("toEmail", configuration["toEmail"]);
Environment.SetEnvironmentVariable("port", configuration["port"]);
Environment.SetEnvironmentVariable("enablessl", configuration["enablessl"]);

Environment.SetEnvironmentVariable("ThumbnailImage", configuration["ThumbnailImage"]);
Environment.SetEnvironmentVariable("ListingImage", configuration["ListingImage"]);
Environment.SetEnvironmentVariable("SmallImage", configuration["SmallImage"]);
Environment.SetEnvironmentVariable("LargeImage", configuration["LargeImage"]);
Environment.SetEnvironmentVariable("PageRendererCache", "Global");

var appId = configuration["appId"];
Environment.SetEnvironmentVariable("appId", !string.IsNullOrEmpty(appId) ? appId : "parentportal");

var culture = configuration["defaultCultureCode"];
var cultureInfo = new CultureInfo(culture ?? "en-US");
var r = new RegionInfo(cultureInfo.Name);

if (cultureInfo.NumberFormat.CurrencySymbol.Trim().Length > 1)
{
    cultureInfo.NumberFormat.CurrencySymbol += " ";
}

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("dbEduegateQueueContext"), new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    }));

builder.Services.AddHangfireServer();

builder.Services.AddWebOptimizer(minifyJavaScript: false, minifyCss: false);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors();

if (configuration["allowonlyhttps"] == "true" && configuration["redirecttowww"] == "true")
{
    app.UseRewriter(new RewriteOptions()
        .AddRedirectToHttps()
        .AddRedirectToWww());
}
else
{
    if (configuration["allowonlyhttps"] == "true")
    {
        app.UseRewriter(new RewriteOptions().AddRedirectToHttps());
    }

    if (configuration["redirecttowww"] == "true")
    {
        app.UseRewriter(new RewriteOptions().AddRedirectToWww());
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/home/error");
    app.UseHsts();
}

app.UseWebOptimizer();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseWebMarkupMin();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Use(async (context, next) =>
{
    var url = context.Request.Path.Value;

    if (context.Request.Path.StartsWithSegments("/robots.txt"))
    {
        var robotsTxtPath = Path.Combine(app.Environment.ContentRootPath, "robots.txt");
        string output = "User-agent: *\nDisallow: /";

        if (File.Exists(robotsTxtPath))
        {
            output = await File.ReadAllTextAsync(robotsTxtPath);
        }

        output = $"{output}Sitemap: {context.Request.Host}/sitemap.xml";
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync(output);
    }
    else
    {
        context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = null;
        await next.Invoke();
    }
});

app.Run();
