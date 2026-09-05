using Bloggy.Application.Contracts.Common;
using System.Security.Claims;

namespace Bloggy.HttpApi.Helpers
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? Id =>
            Guid.TryParse(
                _httpContextAccessor.HttpContext?
                    .User
                    .FindFirstValue(ClaimTypes.NameIdentifier),
                out var userId)
                    ? userId
                    : null;

        public string? Name =>
            _httpContextAccessor.HttpContext?
                .User
                .FindFirstValue("name");

        public string? Email =>
            _httpContextAccessor.HttpContext?
                .User
                .FindFirstValue("email");
    }
}
