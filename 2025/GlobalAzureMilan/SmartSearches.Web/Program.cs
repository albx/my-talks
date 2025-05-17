using Microsoft.Extensions.Azure;
using SmartSearches.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAzureClients(clientBuilder =>
{
    var endpoint = builder.Configuration["AzureSearch:Endpoint"]!;
    var credentialKey = builder.Configuration["AzureSearch:ApiKey"]!;

    clientBuilder.AddSearchIndexClient(
        new(endpoint),
        new Azure.AzureKeyCredential(credentialKey));
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
