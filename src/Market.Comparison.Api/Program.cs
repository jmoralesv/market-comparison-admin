using Market.Comparison.Api;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .AddServiceDefaults()
    .ConfigureServices()
    .ConfigurePipeline();

// Register the endpoints
app.MapWeatherForecastEndpoints();
app.MapDefaultEndpoints();

await app.RunAsync();
