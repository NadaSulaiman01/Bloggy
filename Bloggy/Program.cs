using Bloggy.Application;
using Bloggy.Application.Blogs;
using Bloggy.Application.Contracts.Blogs;
using Bloggy.Application.Contracts.Common;
using Bloggy.Domain;
using Bloggy.EntityFrameworkCore;
using Bloggy.EntityFrameworkCore.Repositories;
using Bloggy.HttpApi.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Bloggy.Application.Contracts.Blogs.RequestDtos;
using Bloggy.Middleware;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddValidatorsFromAssemblyContaining<CreateUpdateBlogRequestDtoValidator>();

builder.Services.AddDbContext<BloggyDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
builder.Services.AddScoped<IBlogService, BlogService>();

builder.Services.AddScoped<ICurrentUser, CurrentUser>();

builder.Services.AddScoped<Func<Guid?>>(sp => () => sp.GetRequiredService<ICurrentUser>().Id);
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
app.UseMiddleware<BusinessExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
