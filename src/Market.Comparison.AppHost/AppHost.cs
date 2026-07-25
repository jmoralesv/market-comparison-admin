var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddConnectionString("MarketComparisonDb");

var auth = builder.AddProject<Projects.Market_Comparison_Auth>("auth")
    .WithReference(sql);

var api = builder.AddProject<Projects.Market_Comparison_Api>("api")
    .WithReference(sql)
    .WithReference(auth);

builder.AddProject<Projects.Market_Comparison_Admin>("admin")
    .WithReference(api)
    .WithReference(auth);

builder.Build().Run();
