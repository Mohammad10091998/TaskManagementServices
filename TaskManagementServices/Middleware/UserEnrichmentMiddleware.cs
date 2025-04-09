using System.Security.Claims;
using TaskManagementServices.Auth;

namespace TaskManagementServices.Middleware
{
    public class UserEnrichmentMiddleware
    {
        private readonly RequestDelegate _next;

        public UserEnrichmentMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserContext userContext)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
                var emailClaim = context.User.FindFirst(ClaimTypes.Email);

                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    userContext.UserId = userId;
                    userContext.Email = emailClaim?.Value ?? string.Empty;
                }
            }

            await _next(context);
        }
    }
}
