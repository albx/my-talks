using BlazorNet10.Web.Models;

namespace BlazorNet10.WebApp.Services;

public class CoursesService(HttpClient httpClient)
{
    public async Task<Course[]> GetCoursesAsync()
    {
        return await httpClient.GetFromJsonAsync<Course[]>("courses") ?? [];
    }
}
