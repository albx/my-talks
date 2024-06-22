using AzureMeetupMilanoSwa.Data;
using Demo1.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

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
    }
}
