using BlazorNet10.WebApp.Components;
using BlazorNet10.WebApp.Services;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Server.Circuits;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.Configure<CircuitOptions>(options =>
{
    options.PersistedCircuitInMemoryMaxRetained = 1;
    options.PersistedCircuitInMemoryRetentionPeriod = TimeSpan.FromMinutes(5);
});

builder.Services.AddValidation();

builder.Services.AddHttpClient<WeatherForecastService>(
    client => client.BaseAddress = new("https+http://blazornet10-webapi"));

builder.Services.AddHttpClient<CoursesService>(
    client => client.BaseAddress = new("https+http://blazornet10-webapi"));

//Metrics and traces
// builder.Services.ConfigureOpenTelemetryMeterProvider(meterProvider =>
// {
//     meterProvider.AddMeter("Microsoft.AspNetCore.Components");
//     meterProvider.AddMeter("Microsoft.AspNetCore.Components.Lifecycle");
//     meterProvider.AddMeter("Microsoft.AspNetCore.Components.Server.Circuits");
// });
// builder.Services.ConfigureOpenTelemetryTracerProvider(tracerProvider =>
// {
//     tracerProvider.AddSource("Microsoft.AspNetCore.Components");
//     tracerProvider.AddSource("Microsoft.AspNetCore.Components.Server.Circuits");
// });

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
