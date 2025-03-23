using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskManagementAPI.Controllers.ControllerAttributes
{
    public class NoAuthRequiredFilter:Attribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAuthenticated = context.HttpContext.User.Identity?.IsAuthenticated??false;
            if (isAuthenticated)
            {
                context.Result = new ForbidResult();
            }
            //Console.WriteLine("Authenticated user:" + isAuthenticated);
        }
    }
}
