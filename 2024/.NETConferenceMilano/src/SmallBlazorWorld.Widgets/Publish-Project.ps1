dotnet publish .\SmallBlazorWorld.Widgets.csproj -c Release -o .\bin\Release\published

Copy-Item -Path ".\bin\Release\published\wwwroot\_framework" -Destination "..\website\public\_framework" -Recurse
Copy-Item -Path ".\bin\Release\published\wwwroot\_content" -Destination "..\website\public\_content" -Recurse 