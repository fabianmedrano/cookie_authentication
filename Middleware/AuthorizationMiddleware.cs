using cookie_authentication.Data;
using Microsoft.EntityFrameworkCore;
namespace cookie_authentication.Middleware
{
    public class AuthorizationMiddleware
    {

       
        private readonly RequestDelegate _next;
        private readonly Dictionary<string, List<string>> _routeRoles = new Dictionary<string, List<string>>
        {
            { "/", new List<string> { "admin", "client" } },
            { "/Home/Privacy", new List<string> { "admin", "client" } },
            // otras rutas y roles...
        };

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                await _next(context);
                return;
            }

            var username = context.User.Identity.Name;
            var user = await dbContext.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.Username == username);

            var userRoles = user?.UserRoles.Select(ur => ur.Role.Name).ToList();

            var path = context.Request.Path.Value.ToLower();
            if (_routeRoles.ContainsKey(path))
            {
                var allowedRoles = _routeRoles[path];
                if (userRoles != null)
                    if (!allowedRoles.Intersect(userRoles).Any())
                    {
                        context.Response.Redirect("/Error/Unauthorized");
                        return;
                    }
                
            }

            await _next(context);
        }
        
    }
}
