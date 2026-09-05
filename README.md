Bloggy
======

Simple blogging API built with ASP.NET Core (.NET 10), EF Core and SQL Server.

Key features
- Domain-driven design architecture with aggregate roots and repository pattern
- EF Core with optimistic concurrency (rowversion)
- Auditing (CreationTime, CreatorId)
- BusinessException middleware with structured error responses
- FluentValidation for request DTOs
- JWT for authentication/authorization using Keycloak identity provider to implement SSO

Projects
- Bloggy (Web API / HTTP entrypoint)
- Bloggy.Application (application services)
- Bloggy.Application.Contracts (DTOs, validators)
- Bloggy.EntityFrameworkCore (EF Core DbContext, configurations, migrations)
- Bloggy.Domain (domain models and repository interfaces)
- Bloggy.Domain.Shared (shared constants and exceptions)

Prerequisites
- .NET 10 SDK
- Visual Studio 2026
- SQL Server (or SQL Server-compatible provider)

Quick start
1. Open project solution in Visual Studio code 2026
2. Build solution
3. Set Bloggy.HttpApi as start up project
4. Open Package Manager Console and set Bloggy.EntityFrameworkCore as default project
5. enter update-database
6. Run start up project



