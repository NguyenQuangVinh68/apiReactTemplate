using Microsoft.Net.Http.Headers;
using apiReact.Services;

namespace apiReact.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;


        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, ITokenService tokenUtil)
        {

            var _bearerToken = context.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var emp_no = tokenUtil.ValidateJwtToken(_bearerToken);

            if (emp_no != null)
            {
                context.Items["User"] = await userService.GetByID(emp_no.Value);
            }
            await _next(context);
        }
    }
}
