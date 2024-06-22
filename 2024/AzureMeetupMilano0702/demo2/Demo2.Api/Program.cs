using AzureMeetupMilanoSwa.Data;
using Demo2.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMeetupsDatabase(
    options => options.ConnectionString = builder.Configuration.GetConnectionString("MeetupDatabase")!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/meetups", GetAllMeetups)
    .WithName(nameof(GetAllMeetups))
    .WithOpenApi();

app.MapGet("/api/meetups/{id:guid}", GetMeetupDetail)
    .WithName(nameof(GetMeetupDetail))
    .WithOpenApi();

app.MapGet("/api/me/meetups", GetMyMeetups)
    .WithName(nameof(GetMyMeetups))
    .WithOpenApi();

app.MapPost("/api/meetups/attend", AttendToMeetup)
    .WithName(nameof(AttendToMeetup))
    .WithOpenApi();

app.Run();

#region Endpoints methods
async Task<IResult> GetAllMeetups(
    MeetupDatabase database)
{
    var meetups = await database.GetAllMeetupsAsync();

    var response = meetups.Select(m => new MeetupListItem(m.Id, m.Title, m.Location, m.Date));
    return Results.Ok(response);
}

async Task<IResult> GetMeetupDetail(
    MeetupDatabase database,
    Guid id)
{
    var meetup = await database.GetMeetupByIdAsync(id);
    if (meetup is null)
    {
        return Results.NotFound();
    }

    var response = new MeetupDetail(meetup.Id, meetup.Title, meetup.Location, meetup.Date);
    return Results.Ok(response);
}

async Task<IResult> GetMyMeetups(
    HttpContext context,
    MeetupDatabase database)
{
    var clientPrincipal = GetClientPrincipalFromHttpRequest(context.Request);
    if (clientPrincipal is null)
    {
        return Results.Unauthorized();
    }

    var meetups = await database.GetMeetupsByAttendeeAsync(clientPrincipal.UserId);

    var response = meetups.Select(m => new MeetupListItem(m.Id, m.Title, m.Location, m.Date));
    return Results.Ok(response);
}

async Task<IResult> AttendToMeetup(
    HttpContext context,
    MeetupDatabase database,
    [FromBody] AttendToMeetupRequest model)
{
    var clientPrincipal = GetClientPrincipalFromHttpRequest(context.Request);
    if (clientPrincipal is null)
    {
        return Results.Unauthorized();
    }

    if (model is null || model.MeetupId == Guid.Empty)
    {
        return Results.BadRequest("Meetup not specified");
    }

    await database.AttendToMeetup(model.MeetupId, clientPrincipal.UserId, clientPrincipal.UserDetails);
    return Results.Ok();
}
#endregion

static ClientPrincipal? GetClientPrincipalFromHttpRequest(HttpRequest request)
{
    if (!request.Headers.TryGetValue("x-ms-client-principal", out var header))
    {
        return null;
    }

    var data = header.First()!;
    var decoded = Convert.FromBase64String(data);
    var json = Encoding.ASCII.GetString(decoded);

    var principal = JsonSerializer.Deserialize<ClientPrincipal>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
    return principal;
}

internal record ClientPrincipal
{
    public string IdentityProvider { get; init; } = string.Empty;

    public string UserId { get; init; } = string.Empty;

    public string UserDetails { get; init; } = string.Empty;

    public string[] UserRoles { get; init; } = [];
}