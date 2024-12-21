using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SmallBlazorWorld.App.Shared.Components;
using SmallBlazorWorld.App.Shared.Services;
using SmallBlazorWorld.Widgets;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
//builder.RootComponents.Add<App>("#app");
//builder.RootComponents.Add<HeadOutlet>("head::after");

builder.RootComponents.RegisterCustomElement<MyEvents>("my-events");

builder.Services.AddHttpClient<IEventsService, EventsService>(
    client => client.BaseAddress = new("http://localhost:5296"));

await builder.Build().RunAsync();
