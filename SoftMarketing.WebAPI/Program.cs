using Microsoft.AspNetCore.HttpOverrides;
using SoftMarketing.Model;
using SoftMarketing.Model.RedisModels;
using SoftMarketing.Services.Sales;
using SoftMarketing.WebAPI;
using SoftMarketing.WebAPI.Helpers;
using SoftMarketing.WebAPI.Security;
using StackExchange.Redis;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.AddFilter("Microsoft.AspNetCore.SignalR", LogLevel.Debug);
builder.Logging.AddFilter("Microsoft.AspNetCore.Http.Connections", LogLevel.Debug);

builder.Services.AddAuthentication();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
options.ForwardedHeaders =
    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;

    services.AddCors();
    services.AddControllers()
        .AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

    builder.Services.AddSignalR(hubOptions =>
    {
        hubOptions.EnableDetailedErrors = true;
        hubOptions.KeepAliveInterval = TimeSpan.FromSeconds(10);
        hubOptions.HandshakeTimeout = TimeSpan.FromSeconds(10);
    });

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetSection("AppSettings").GetConnectionString("Redis")));

    builder.Services.AddSwaggerGen();
    // configure DI for application services
    services.AddScoped<IJwtUtils, JwtUtils>();
    builder.Services.AddSignalR();
    services.AddScoped<IPlatformRepo, RedisPlatformRepo>();
    services.AddScoped<IUserManager, UserManager>();
    services.AddScoped<IUserService, UserService>();
}
var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();
app.UseWebSockets();

// configure HTTP request pipeline
{
    //app.Environment.IsDevelopment() disables swagger when going live. If app.Environment.IsDevelopment() is commented
    //then Server API can be checked after publishing at: https://api.business1.app/swagger/index.html
    //But then Swagger API is exposed to everyone. So, use with caution
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    // global cors policy
    app.UseCors(x => x
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

    // global error handler
    //app.UseMiddleware<ErrorHandlerMiddleware>();

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHub<ServerHub>("/datahub");
    });
    app.MapControllers();
    app.UseStaticFiles();
}

app.Run();

// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// configure strongly typed settings object
//builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
//// configure strongly typed settings object

//// configure DI for application services
//builder.Services.AddScoped<IJwtUtils, JwtUtils>();
//builder.Services.AddScoped<SoftMarketing.Service.IUserService, SoftMarketing.Service.UserService>();
//builder.Services.AddScoped<SoftMarketing.WebAPI.Security.IUserService, SoftMarketing.WebAPI.Security.UserService>();
//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//// configure HTTP request pipeline
//{
//    // global cors policy
//    app.UseCors(x => x
//        .SetIsOriginAllowed(origin => true)
//        .AllowAnyMethod()
//        .AllowAnyHeader()
//        .AllowCredentials());

//    // global error handler
//    app.UseMiddleware<ErrorHandlerMiddleware>();

//    // custom jwt auth middleware
//    app.UseMiddleware<JwtMiddleware>();

//    app.MapControllers();
//}

////app.UseAuthorization();
////app.UseMiddleware<ErrorHandlerMiddleware>();
////app.UseMiddleware<JwtMiddleware>();
////app.MapControllers();

