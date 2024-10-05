using Newtonsoft.Json;
using PioConnection.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
var endpoints = builder.GetEndpoints();
builder.Services.RegisterEndpointServices(endpoints);
builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });
var app = builder.Build();

app.RegisterEndpoints(endpoints);
app.Run();