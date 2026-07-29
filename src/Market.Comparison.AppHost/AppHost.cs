var builder = DistributedApplication.CreateBuilder(args);

var authDatabase = builder.AddConnectionString("AuthConnection");

var auth = builder.AddProject<Projects.Market_Comparison_Auth>("auth")
    .WithReference(authDatabase);

var api = builder.AddProject<Projects.Market_Comparison_Api>("api")
    .WithReference(auth);

builder.AddProject<Projects.Market_Comparison_Admin>("admin")
    .WithReference(api)
    .WithReference(auth);

builder.Build().Run();
