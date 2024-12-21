// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmallBlazorWorld.ConsoleApp.Templates;
using SmallBlazorWorld.Core;
using SmallBlazorWorld.Core.Models;
using SmallBlazorWorld.Core.Services;

Console.WriteLine("Hello, Blazor World!");

var services = new ServiceCollection();
services.AddLogging();

var serviceProvider = services.BuildServiceProvider();

try
{
    var dbContextOptions = new DbContextOptionsBuilder<TalkManagerDbContext>()
        .UseSqlServer("Server=.\\SQLEXPRESS;Database=TalkManager;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True")
        .Options;

    var talkDbContext = new TalkManagerDbContext(dbContextOptions);

    var database = new Database(talkDbContext);
    var commands = new TalkCommands(talkDbContext);

    Console.WriteLine("Approve all talks and create a confirmation email");

    var proposalsToApprove = FindProposalsToApprove(database);

    foreach (var proposal in proposalsToApprove)
    {
        await commands.ApproveTalkAsync(proposal.Talk.Id, proposal.Event.Id);
        await CreateConfirmationEmailAsync(proposal.Talk.Title, proposal.Event.Name, proposal.Event.Location, proposal.Event.Date);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}


Proposal[] FindProposalsToApprove(Database database)
{
    var proposalsToApprove = database.Proposals
        .Where(p => p.State == Proposal.ProposalState.PendingApproval)
        .ToArray();

    return proposalsToApprove;
}

async Task CreateConfirmationEmailAsync(string talkTitle, string eventName, string eventLocation, DateOnly eventDate)
{
    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
    await using var htmlRenderer = new HtmlRenderer(serviceProvider, loggerFactory);

    var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
    {
        var parametersDictionary = new Dictionary<string, object>
        {
            [nameof(TalkConfirmed.Model)] = new TalkConfirmed.Emailodel(talkTitle, eventName, eventLocation, eventDate)
        };

        var parameters = ParameterView.FromDictionary(parametersDictionary!);
        var output = await htmlRenderer.RenderComponentAsync<TalkConfirmed>(parameters);

        return output.ToHtmlString();
    });

    var fileDirectory = "Emails";
    if (!Directory.Exists(fileDirectory))
    {
        Directory.CreateDirectory(fileDirectory);
    }

    var filePath = Path.Combine(
        fileDirectory, 
        $"{talkTitle.Replace(" ", "").Replace(".", "").Replace(":", "")}_{eventName.Replace(" ", "").Replace(".", "")}_{eventDate:yyyyMMdd}.html");

    File.WriteAllText(filePath, html);
}