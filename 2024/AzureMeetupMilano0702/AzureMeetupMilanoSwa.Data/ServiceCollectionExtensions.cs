using Microsoft.Extensions.DependencyInjection;

namespace AzureMeetupMilanoSwa.Data;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMeetupsDatabase(this IServiceCollection services, Action<MeetupDatabase.MeetupDatabaseOptions> configureOptions)
    {
        services.Configure(configureOptions);
        services.AddScoped<MeetupDatabase>();

        return services;
    }
}
