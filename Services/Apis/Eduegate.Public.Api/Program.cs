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
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.School.Models.School;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SignalRChat.Hubs;
using Eduegate.Domain.Entity.Contents;
using Eduegate.Services.CRM;
using AppOwnsData.Services;
using AadService = AppOwnsData.Services.AadService;
using AppOwnsData.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
    //options.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ss.sssZ";
});

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
});

builder.Services.AddSwaggerGen(x => x.SwaggerDoc("v1", new OpenApiInfo() { Title = "Eduegate.Public.Api", Version = "v1" }));
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

//Environment.SetEnvironmentVariable("integrationDbContext", builder.Configuration.GetConnectionString("integrationDbContext"));

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

builder.Services.Configure<AzureAd>(options =>
{
    options.AuthenticationMode = "ServicePrincipal";
    options.AuthorityUrl = "https://login.microsoftonline.com/organizations/";
    options.ClientId = "27722e62-988d-4f64-a8cc-62b49eef9286";
    options.TenantId = "74371ba3-a603-4a16-ae30-9741f9381eaa";
    options.ScopeBase = new[] { "https://analysis.windows.net/powerbi/api/.default" };
    options.PbiUsername = ""; // Leave empty for Service Principal
    options.PbiPassword = ""; // Leave empty for Service Principal
    options.ClientSecret = "VEN8Q~4rMlee69VEQM1spjQ.V3b6R9Z_hvGKAcc5";
});



builder.Services.AddHangfireServer();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<RealtimeClient>();
builder.Services.AddScoped<PowerBIService>();
builder.Services.AddTransient<PbiEmbedService>();
builder.Services.AddTransient<AadService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
})
.AddCookie(options =>
{
    options.Cookie.Name = "EduEgatePublicAPI";
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.ExpireTimeSpan = TimeSpan.FromHours(5);
    options.SlidingExpiration = true;
    //options.LoginPath = "/Account/Login";
    //options.LogoutPath = "/Account/Logout/";
    //options.Cookie.HttpOnly = true;
    //options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddSignalR();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigins",
//        policy =>
//        {
//            policy.WithOrigins("http://localhost:8000") // Allow the origin you need
//                  .AllowAnyHeader()
//                  .AllowAnyMethod()
//                  .AllowCredentials();
//            policy.WithOrigins("http://localhost:8001") // Allow the origin you need
//                .AllowAnyHeader()
//                .AllowAnyMethod()
//                .AllowCredentials();
//        });
//});
var app = builder.Build();
//app.UseCors("AllowSpecificOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Eduegate.Public.Api v1");
    });
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseHangfireDashboard();
app.UseAuthentication();

//app.Use(async (context, next) =>
//{
//    var isOffline = new Domain.Setting.SettingBL(null).GetSettingValue<bool>("API_OFFLINE_MODE", false);

//    if (isOffline)
//    {
//        context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
//        await context.Response.WriteAsync("The application is currently offline for maintenance.");
//        return;
//    }

//    await next.Invoke();
//});

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features
        .Get<IExceptionHandlerPathFeature>()
        .Error;
    var response = new { error = exception.Message };
    Eduegate.Logger.LogHelper<Program>.Fatal(exception.Message, exception);
    await context.Response.WriteAsJsonAsync(response);
}));

app.UseAuthorization();
app.MapHangfireDashboard();
app.MapControllers();
app.UseEndpoints(endpoints =>
{
    _=endpoints.MapHub<ChatHub>("/api/communication/chathub");
});
app.Run();