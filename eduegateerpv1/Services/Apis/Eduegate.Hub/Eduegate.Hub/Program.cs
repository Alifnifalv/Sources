using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Diagnostics;
using Eduegate.Hub.Client;
using System.Net;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Hub;
using Eduegate.Domain.Entity.Contents;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Routing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
});
builder.Services    .Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
       builder => builder
           .AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod()
       );
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:8000") // Allow the origin you need
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
            policy.WithOrigins("http://localhost:8001") // Allow the origin you need
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});
builder.Services.AddSwaggerGen(x => x.SwaggerDoc("v1", new OpenApiInfo() { Title = "Eduegate.Hub", Version = "v1" }));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(options =>
             {
                 options.LoginPath = "/Account/Login/";
                 options.LogoutPath = "/Account/Logout/";
             });
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddMvc()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });

Environment.SetEnvironmentVariable("dbEduegateERPContext", builder.Configuration.GetConnectionString("dbEduegateERPContext"));
Environment.SetEnvironmentVariable("dbEduegateSchoolContext", builder.Configuration.GetConnectionString("dbEduegateSchoolContext"));
Environment.SetEnvironmentVariable("dbContentContext", builder.Configuration.GetConnectionString("dbContentContext"));
Environment.SetEnvironmentVariable("EduegatedERP_LoggerContext", builder.Configuration.GetConnectionString("EduegatedERP_LoggerContext"));

var appSettings = builder.Configuration.GetSection("AppSettings");
Environment.SetEnvironmentVariable("smtphost", appSettings["smtphost"]);
Environment.SetEnvironmentVariable("smtpemail", appSettings["smtpemail"]);
Environment.SetEnvironmentVariable("smtppassword", appSettings["smtppassword"]);
Environment.SetEnvironmentVariable("fromEmail", appSettings["fromEmail"]);
Environment.SetEnvironmentVariable("toEmail", appSettings["toEmail"]);
Environment.SetEnvironmentVariable("port", appSettings["port"]);
Environment.SetEnvironmentVariable("enablessl", appSettings["enablessl"]);
Environment.SetEnvironmentVariable("PageRendererCache", "Global");

builder.Services.AddDbContext<dbEduegateERPContext>(options =>
{
    options.UseSqlServer(Eduegate.Infrastructure.ConfigHelper.GetDefaultConnectionString());
});

builder.Services.AddDbContext<dbContentContext>(options =>
{
    options.UseSqlServer(Eduegate.Infrastructure.ConfigHelper.GetContentConnectionString());
});

builder.Services.AddDbContext<dbEduegateSchoolContext>(options =>
{
    options.UseSqlServer(Eduegate.Infrastructure.ConfigHelper.GetSchoolConnectionString());
});

builder.Services.AddHangfire(configuration => configuration
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
builder.Services.AddControllers();
builder.Services.AddSingleton<RealtimeClient>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Eduegate.Hub v1");
    });
}

app.UseStaticFiles();
//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll"); // Use CORS policy
app.UseHangfireDashboard();

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features
        .Get<IExceptionHandlerPathFeature>()
        .Error;
    var response = new { error = exception.Message };
    await context.Response.WriteAsJsonAsync(response);
}));



app.MapControllers();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapHub<ChatHub>("/api/communication/chathub");
});
app.Run();
