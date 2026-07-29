using Market.Comparison.Admin;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .AddServiceDefaults()
    .ConfigureServices()
    .ConfigurePipeline();

app.MapDefaultEndpoints();

await app.RunAsync();
