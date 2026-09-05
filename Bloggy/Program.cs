using Bloggy.Application;
using Bloggy.Application.Blogs;
using Bloggy.Application.Contracts.Blogs;
using Bloggy.Domain;
using Bloggy.EntityFrameworkCore;
using Bloggy.EntityFrameworkCore.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<BloggyDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<BlogProfile>();
});

var corsOrigins = builder.Configuration["App:CorsOrigins"]?
    .Split(",", StringSplitOptions.RemoveEmptyEntries)
    .Select(s => s.Trim().TrimEnd('/'))
    .ToArray()
    ?? Array.Empty<string>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicy =>
    {
        corsPolicy
            .WithOrigins(corsOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Keycloak:Authority"];

        options.Audience = builder.Configuration["Keycloak:Audience"];

        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/openapi/v1.json",
            "My API v1");
    });
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
