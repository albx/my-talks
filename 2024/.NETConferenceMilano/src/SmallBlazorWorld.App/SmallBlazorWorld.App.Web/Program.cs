using Microsoft.EntityFrameworkCore;
using SmallBlazorWorld.App.Shared.Services;
using SmallBlazorWorld.App.Web.Components;
using SmallBlazorWorld.App.Web.Services;
using SmallBlazorWorld.Core;
using SmallBlazorWorld.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<TalkManagerDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddScoped<Database>();
builder.Services.AddScoped<TalkCommands>();

// Add device-specific services used by the SmallBlazorWorld.App.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddScoped<IEventsService, EventsService>();

builder.Services.AddCors(policy => {
    policy.AddDefaultPolicy(builder => {
        builder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseCors();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(SmallBlazorWorld.App.Shared._Imports).Assembly);

#region API Endpoints
app.MapGet("api/my-events", async (IEventsService service) =>
{
    var myEvents = await service.GetMyEventsAsync();
    return Results.Ok(myEvents);
});
#endregion

app.Run();
