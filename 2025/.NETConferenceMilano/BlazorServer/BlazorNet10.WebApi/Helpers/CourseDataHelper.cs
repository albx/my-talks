using BlazorNet10.Web.Models;

namespace BlazorNet10.WebApi.Helpers;

public static class CourseDataHelper
{
    public static List<Course> GetAllCourses()
    {
        return new List<Course>
        {
            new Course
            {
                Title = "Getting Started with .NET 10",
                Description = "Learn the fundamentals of .NET 10 and explore the latest features and improvements in this comprehensive beginner course."
            },
            new Course
            {
                Title = "Advanced C# Programming Techniques",
                Description = "Master advanced C# concepts including LINQ, async/await patterns, generics, and modern language features."
            },
            new Course
            {
                Title = "Building Web APIs with ASP.NET Core",
                Description = "Create robust and scalable RESTful APIs using ASP.NET Core with authentication, validation, and best practices."
            },
            new Course
            {
                Title = "Blazor Server Applications Development",
                Description = "Build interactive web applications using Blazor Server with real-time updates and component-based architecture."
            },
            new Course
            {
                Title = "Blazor WebAssembly Fundamentals",
                Description = "Develop client-side web applications with Blazor WebAssembly and learn to leverage C# in the browser."
            },
            new Course
            {
                Title = "Azure App Service Deployment Strategies",
                Description = "Deploy and manage .NET applications on Azure App Service with CI/CD pipelines and scaling configurations."
            },
            new Course
            {
                Title = "Azure Functions for Serverless Computing",
                Description = "Build serverless applications with Azure Functions using C# and integrate with various Azure services."
            },
            new Course
            {
                Title = "Entity Framework Core Deep Dive",
                Description = "Master database operations in .NET with Entity Framework Core, including migrations, relationships, and performance optimization."
            },
            new Course
            {
                Title = "Microservices Architecture with .NET",
                Description = "Design and implement microservices using .NET, Docker containers, and communication patterns."
            },
            new Course
            {
                Title = "Azure Cosmos DB with .NET Applications",
                Description = "Integrate Azure Cosmos DB into your .NET applications and learn NoSQL database design patterns."
            },
            new Course
            {
                Title = "Authentication and Authorization in ASP.NET Core",
                Description = "Implement secure authentication and authorization using Identity, JWT tokens, and OAuth in ASP.NET Core applications."
            },
            new Course
            {
                Title = "Azure Service Bus Messaging Patterns",
                Description = "Build reliable messaging solutions using Azure Service Bus with queues, topics, and subscription patterns."
            },
            new Course
            {
                Title = "Performance Optimization in .NET Applications",
                Description = "Identify and resolve performance bottlenecks in .NET applications using profiling tools and optimization techniques."
            },
            new Course
            {
                Title = "Azure Key Vault Security Integration",
                Description = "Secure your .NET applications by integrating Azure Key Vault for secrets, keys, and certificate management."
            },
            new Course
            {
                Title = "SignalR for Real-time Web Applications",
                Description = "Add real-time functionality to web applications using SignalR with hubs, groups, and scaling considerations."
            },
            new Course
            {
                Title = "Azure DevOps CI/CD for .NET Projects",
                Description = "Set up continuous integration and deployment pipelines for .NET applications using Azure DevOps Services."
            },
            new Course
            {
                Title = "Docker Containerization for .NET Apps",
                Description = "Containerize .NET applications with Docker and deploy them to Azure Container Instances and Kubernetes."
            },
            new Course
            {
                Title = "Azure SQL Database with .NET Integration",
                Description = "Connect and optimize .NET applications with Azure SQL Database, including connection pooling and security."
            },
            new Course
            {
                Title = "Testing Strategies for .NET Applications",
                Description = "Implement comprehensive testing strategies including unit tests, integration tests, and automated testing with xUnit."
            },
            new Course
            {
                Title = "Azure Application Insights Monitoring",
                Description = "Monitor and diagnose .NET applications in production using Azure Application Insights with custom telemetry and alerts."
            }
        };
    }

    public static Course[] GetRandomCourses(int count = 10)
    {
        var allCourses = GetAllCourses();
        return allCourses.OrderBy(c => Random.Shared.Next()).Take(count).OrderBy(c => c.Title).ToArray();
    }
}