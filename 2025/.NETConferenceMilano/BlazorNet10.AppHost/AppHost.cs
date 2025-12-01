var builder = DistributedApplication.CreateBuilder(args);

var webapi = builder.AddProject<Projects.BlazorNet10_WebApi>("blazornet10-webapi");

builder.AddProject<Projects.BlazorNet10_WebApp>("blazornet10-webapp")
    .WithReference(webapi)
    .WaitFor(webapi);

builder.Build().Run();
