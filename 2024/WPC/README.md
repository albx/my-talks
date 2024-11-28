# WPC 2024 - FluentUI vs MudBlazor: il grande match

This is the talk (Italian language) I've done at [WPC 2024](https://www.wpc.education/)
In this talk I made a comparison beetween [FluentUI](https://www.fluentui-blazor.net/) and [MudBlazor](https://www.mudblazor.com/)

## Requirements

.NET 9 SDK must be installed.
SQL Server express installed

## Usage
In the *src* folder there are two main project:
- The [BlazorUIWars.FluentApp](./src/BlazorUIWars.FluentUIApp/) is a simple Expense report app which uses FluentUI as UI-kit
- The [BlazorUIWars.MudBlazorApp](./src/BlazorUIWars.MudBlazorApp/) is the same application made using MudBlazor.

Before run one of this project, please execute the Entity Framework Core migrations located in the *BlazorUIWars.Core* project.

Just execute `dotnet run` command to execute the projects.
