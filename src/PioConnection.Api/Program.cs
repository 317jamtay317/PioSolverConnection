using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using PioConnection.Api.Extensions;
using PioConnection.Api.Logging;
using PioConnection.Api.Services;
using Serilog;
using ILogger = Serilog.ILogger;

ILogger logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
#if DEBUG
    .WriteTo.Debug()
    .WriteTo.Console()
#else
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
#endif
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(logger);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PioConnection API",
        Version = "v1",
        Description = "API for PioConnection application"
    });
});
var endpoints = builder.GetEndpoints();
builder.Services.RegisterEndpointServices(endpoints);
 builder.Services
     .AddControllers()
     .AddNewtonsoftJson(options =>
     {
         options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
     });
builder.Services.AddScoped(typeof(ILoggerWrapper<>), typeof(LoggerWrapper<>));
builder.Services.AddScoped<IRangeService, RangeService>();
var app = builder.Build();
app.MapGet("/api/helloword", () => "Hello World!")
    .WithName("HelloWorld")
    .WithTags("TestingApi")
    .Produces<string>();
app.RegisterEndpoints(endpoints);
if (app.Environment.IsDevelopment()) // Ensure Swagger is enabled only in development
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PioConnection API v1");
        c.RoutePrefix = "swagger"; // Ensures Swagger is served at root (http://localhost:5193/)
    });
} ;

app.Run();