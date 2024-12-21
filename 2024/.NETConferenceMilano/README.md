# .NET Conference 2024 - Blazor tutti i gusti più uno

This is the talk (Italian language) I've done at [.NET Conference 2024](https://dotnetconference.it/).

In this talk I showed three uses of Blazor which are not related to an ASP.NET Core application.

- The first was the use of Blazor in a .NET Maui application, using Blazor Hybrid and the [new ".NET Maui Blazor and Web app" template](https://learn.microsoft.com/en-us/aspnet/core/blazor/hybrid/tutorials/maui-blazor-web-app?view=aspnetcore-9.0)
- The second was the [use of Blazor in a console application](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/render-components-outside-of-aspnetcore?view=aspnetcore-9.0). This approach allows to render a Razor component as string
- The third was the use of [Blazor custom elements](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/js-spa-frameworks?view=aspnetcore-9.0)

## Requirements

- .NET 9 SDK must be installed.
- SQL Server express installed
- .NET Maui workloads installed
- NodeJS installed

## Usage

Be sure to run the Entity Framework `Update-Database` command using the *SmallBlazorWorld.Core* project as default project and *SmallBlazorWorld.App.Web* as startup project.

The .NET Maui project is located in the folder SmallBlazorWorld.App/SmallBlazorWorld.App and uses a in-memory persistence, so you just need to choose the target and run and it would just works.

The Web project is located in the folder SmallBlazorWorld.App/SmallBlazorWorld.App.Web. Once created the database just run `dotnet run` and you can start using the project.

The console application is located in the folder SmallBlazorWorld.ConsoleApp. Just run it and you can find the HTML generated using the Razor component in the *bin/Debug/net9.0/Emails* folder.

To try the Blazor custom elements, open the *website* folder and run `npm install`.
Then you can run the powershell script `Publish-Project.ps1` which can be found in the *SmallBlazorWorld.Widgets* folder. Once the command is terminated you can go back to the *website* folder and run `npm run start` to see the react application showing the Blazor component as HTML Web Component.