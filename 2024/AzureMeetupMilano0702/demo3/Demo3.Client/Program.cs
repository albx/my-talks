using Demo3.Client;
using Demo3.Client.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient(
    "AuthClient",
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services
    .AddAuthorizationCore()
    .AddScoped<AuthenticationStateProvider, StaticWebAppsAuthenticationStateProvider>();

await builder.Build().RunAsync();