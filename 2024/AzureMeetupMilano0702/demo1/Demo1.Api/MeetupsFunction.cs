using AzureMeetupMilanoSwa.Data;
using Demo1.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Demo1.Api
{
    public class MeetupsFunction
    {
        private readonly ILogger<MeetupsFunction> _logger;

        private readonly MeetupDatabase _database;

        public MeetupsFunction(ILogger<MeetupsFunction> logger, MeetupDatabase database)
        {
            _logger = logger;
            _database = database;
        }

        [Function(nameof(GetAllMeetups))]
        public async Task<IActionResult> GetAllMeetups(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "meetups")] HttpRequest request)
        {
            var meetups = await _database.GetAllMeetupsAsync();

            var response = meetups.Select(m => new MeetupListItem(m.Id, m.Title, m.Location, m.Date));
            return new OkObjectResult(response);
        }

        [Function(nameof(GetMeetupDetail))]
        public async Task<IActionResult> GetMeetupDetail(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "meetups/{id}")] HttpRequest request,
            Guid id)
        {
            var meetup = await _database.GetMeetupByIdAsync(id);
            if (meetup is null)
            {
                return new NotFoundObjectResult(null);
            }

            var response = new MeetupDetail(meetup.Id, meetup.Title, meetup.Location, meetup.Date);
            return new OkObjectResult(response);
        }

        [Function(nameof(GetMyMeetups))]
        public async Task<IActionResult> GetMyMeetups(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "me/meetups")] HttpRequest request)
        {
            var clientPrincipal = GetClientPrincipalFromHttpRequest(request);
            if (clientPrincipal is null)
            {
                return new UnauthorizedObjectResult(null);
            }

            var meetups = await _database.GetMeetupsByAttendeeAsync(clientPrincipal.UserId);

            var response = meetups.Select(m => new MeetupListItem(m.Id, m.Title, m.Location, m.Date));
            return new OkObjectResult(response);
        }

        [Function(nameof(AttendToMeetup))]
        public async Task<IActionResult> AttendToMeetup(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "meetups/attend")] HttpRequest request)
        {
            var clientPrincipal = GetClientPrincipalFromHttpRequest(request);
            if (clientPrincipal is null)
            {
                return new UnauthorizedObjectResult(null);
            }

            var model = await request.ReadFromJsonAsync<AttendToMeetupRequest>();
            if (model is null || model.MeetupId == Guid.Empty)
            {
                var modelState = new ModelStateDictionary();
                modelState.AddModelError(nameof(AttendToMeetupRequest.MeetupId), "Meetup not specified");

                return new BadRequestObjectResult(modelState);
            }

            await _database.AttendToMeetup(model.MeetupId, clientPrincipal.UserId, clientPrincipal.UserDetails);
            return new OkObjectResult(null);
        }

        private static ClientPrincipal? GetClientPrincipalFromHttpRequest(HttpRequest request)
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
    }
}
