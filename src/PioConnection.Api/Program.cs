using System.Reflection;
using FluentValidation;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PioConnection.Api.Core;
using PioConnection.Api.Core.Swagger;
using PioConnection.Api.Core.Validation;
using PioConnection.Api.Dtos;
using PioConnection.Api.Logging;
using PioConnection.Api.Services;
using PioConnection.Dtos;
using Serilog;
using ILogger = Serilog.ILogger;

ILogger logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
#if DEBUG
    .WriteTo.Debug()
#else
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
#endif
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(logger);

// Path to the XML documentation file
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";  // XML file based on the assembly name
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);  // Path to the XML file

// Add Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PioConnection API",
        Version = "v1",
        Description = "API for PioConnection application"
    });

    // Include XML comments
    c.IncludeXmlComments(xmlPath);

    // Use StringEnumConverter for Swagger as well
    c.SchemaFilter<EnumSchemaFilter>(); // Optional if you want descriptions for enum values

    // Treat enums as strings in Swagger definitions
    c.MapType<Street>(() => new OpenApiSchema
    {
        Type = "string",
        Enum = Enum.GetNames(typeof(Street))
            .Select(e => new OpenApiString(e))
            .Cast<IOpenApiAny>()
            .ToList()
    });

    c.MapType<ActionType>(() => new OpenApiSchema
    {
        Type = "string",
        Enum = Enum.GetNames(typeof(ActionType))
            .Select(e => new OpenApiString(e))
            .Cast<IOpenApiAny>()
            .ToList()
    });

    // Inline enum definitions within schema
    c.SchemaGeneratorOptions.UseInlineDefinitionsForEnums = true;
});


// Register services
builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });
builder.Services.AddValidatorsFromAssemblyContaining<FlopRangeRequestValidator>();
builder.Services.AddTransient(typeof(ILoggerWrapper<>), typeof(LoggerWrapper<>));
builder.Services.AddScoped<IRangeService, RangeService>();
builder.Services.AddSingleton<ISolverConnectionFactory, SolverConnectionFactory>();
builder.Services.AddSingleton<ISolverFileService, SolverFileService>();
builder.Services.AddSingleton<IBetSizingService, BetSizingService>();
var app = builder.Build();
logger.Information("Hello this is working");
// Map controllers
app.MapControllers();
var logWrapper = app.Services.GetRequiredService<ILoggerWrapper<Program>>();
logWrapper.Debug("LogWrapper working");
logger.Information("Current environment: {Environment}", app.Environment.EnvironmentName);
// Configure Swagger

logger.Information("Configuring Swagger...");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PioConnection API v1");
    c.RoutePrefix = "swagger";
});
logger.Information("Swagger configuration completed.");

app.Run();

public partial class Program
{
    
}