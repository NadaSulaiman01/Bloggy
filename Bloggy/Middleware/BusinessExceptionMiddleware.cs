using Bloggy.Domain.Shared;
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
