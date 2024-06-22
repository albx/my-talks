using AzureMeetupMilanoSwa.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((ctx, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddMeetupsDatabase(options => options.ConnectionString = ctx.Configuration["MeetupDatabase"]!);
    })
    .Build();

host.Run();