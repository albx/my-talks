using BlazorNet10.Web.Models;

namespace BlazorNet10.WebApp.Services;

public class WeatherForecastService(HttpClient httpClient)
{
    public async Task<WeatherForecast[]> GetWeatherAsync()
    {
        // Simulate asynchronous loading to demonstrate streaming rendering
        return await httpClient.GetFromJsonAsync<WeatherForecast[]>("weatherforecast") ?? [];
    }
}
