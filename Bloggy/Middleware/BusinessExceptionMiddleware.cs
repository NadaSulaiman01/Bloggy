using Bloggy.Domain.Shared;
using FluentValidation;
using System.Text.Json;

namespace Bloggy.Middleware
{
    public sealed class BusinessExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public BusinessExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var errors = ex.Errors.Select(e => new { field = e.PropertyName, error = e.ErrorMessage });
                var payload = new { message = "Validation failed", errors };
                var json = JsonSerializer.Serialize(payload);
                await context.Response.WriteAsync(json);
            }
            catch (BusinessException ex)
            {
                var status = StatusCodes.Status403Forbidden;

                context.Response.StatusCode = status;
                context.Response.ContentType = "application/json";

                var payload = new { code = ex.Code, message = ex.Message };
                var json = JsonSerializer.Serialize(payload);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
