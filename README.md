Bloggy
======

Simple blogging sample API built with ASP.NET Core (.NET 10), EF Core and SQL Server.

Key features
- Clean domain model with aggregate roots and repository pattern
- EF Core with optimistic concurrency (rowversion)
- Auditing (CreationTime, CreatorId)
- BusinessException middleware with structured error responses
- FluentValidation for request DTOs

Projects
- Bloggy (Web API / HTTP entrypoint)
- Bloggy.Application (application services)
- Bloggy.Application.Contracts (DTOs, validators)
- Bloggy.EntityFrameworkCore (EF Core DbContext, configurations, migrations)
- Bloggy.Domain / Bloggy.Domain.Shared (domain model and shared constants)

Prerequisites
- .NET 10 SDK
- SQL Server (or SQL Server-compatible provider)

Quick start
1. Update connection string in Bloggy/appsettings.json (DefaultConnection)
2. Restore packages from solution root:
   dotnet restore
3. Apply EF Core migrations and update the database:
   dotnet ef database update --project Bloggy.EntityFrameworkCore --startup-project Bloggy
4. Run the API from the Bloggy project folder:
   dotnet run --project Bloggy

API
- Base route: /api/blogs
- Endpoints implemented in Bloggy.HttpApi.Controllers.BlogsController (create, update, delete, list, list current user)

Authentication
- JWT authentication is configured in Program.cs. Configure Keycloak settings (Keycloak:Authority, Keycloak:Audience) in appsettings if you want to use auth-protected endpoints.

Notes
- Business errors are returned as JSON { code, message } by the BusinessException middleware.
- To add localization for messages replace the ErrorMessages class with an IStringLocalizer-based implementation.
