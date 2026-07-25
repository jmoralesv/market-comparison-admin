using Market.Comparison.Api;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

// Register the endpoints
app.MapWeatherForecastEndpoints();
app.MapDefaultEndpoints();

await app.RunAsync();
