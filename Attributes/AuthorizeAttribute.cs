using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;

namespace apiReact.Attributes
{
    public class AuthorizeAttribute { }
    //public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    //{
    //    private readonly IList<Role> _role;

    //    public AuthorizeAttribute(params Role[] role)
    //    {
    //        _role = role ?? new Role[] { };
    //    }

    //    public void OnAuthorization(AuthorizationFilterContext context)
    //    {
    //        var user = (User)context.HttpContext.Items["User"];

    //        if (user == null || (_role.Any() && !_role.Contains(user.Role)))
    //        {
    //            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
    //        }
    //    }
    //}
}
