# **Bloggy**

Simple blogging API built with **ASP.NET Core (.NET 10)**, **EF Core**, and **SQL Server**.

## **Key Features**

* **Domain-driven design** architecture with aggregate roots and repository pattern
* **EF Core** with optimistic concurrency using `rowversion`
* **Auditing** (`CreationTime`, `CreatorId`)
* **BusinessException middleware** with structured error responses
* **FluentValidation** for request DTOs
* **JWT authentication/authorization** using **Keycloak** as the identity provider for SSO

## **Projects**

* **Bloggy** — Web API / HTTP entrypoint
* **Bloggy.Application** — Application services
* **Bloggy.Application.Contracts** — DTOs and validators
* **Bloggy.EntityFrameworkCore** — EF Core DbContext, configurations, and migrations
* **Bloggy.Domain** — Domain models and repository interfaces
* **Bloggy.Domain.Shared** — Shared constants and exceptions

## **Prerequisites**

* **.NET 10 SDK**
* **SQL Server** (or SQL Server-compatible provider)
* **Keycloak** — see the [Bloggy Infrastructure](https://github.com/) repository

## **Quick Start**

From the **solution root**, run the following commands.

### **1. Restore dependencies**

```bash
dotnet restore
```

### **2. Update the database**

```bash
dotnet ef database update --project Bloggy.EntityFrameworkCore --startup-project Bloggy
```

### **3. Run the API**

```bash
dotnet run --project Bloggy --launch-profile https
```

The API will be available at:

```text
https://localhost:7084
```
