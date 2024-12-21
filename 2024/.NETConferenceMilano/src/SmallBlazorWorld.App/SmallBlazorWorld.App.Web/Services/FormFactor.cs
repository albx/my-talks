using SmallBlazorWorld.App.Shared.Services;

namespace SmallBlazorWorld.App.Web.Services;
public class FormFactor : IFormFactor
{
    public string GetFormFactor()
    {
        return "Web";
    }

    public string GetPlatform()
    {
        return Environment.OSVersion.ToString();
    }
}
