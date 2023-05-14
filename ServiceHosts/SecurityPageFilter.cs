using System.Linq;
using System.Reflection;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ServiceHosts
{
    public class SecurityPageFilter:IPageFilter
    {
        private readonly IAuthHelper _authHelper;

        public SecurityPageFilter(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var permissionHandler =
                 (NeedsPermissionAttribute) context.HandlerMethod.MethodInfo.GetCustomAttribute(typeof(NeedsPermissionAttribute));
            if (permissionHandler ==null)
                return;
            var accountPermission = _authHelper.GetPermissions();
            if (accountPermission.All(x => x != permissionHandler.Permission)) 
                context.HttpContext.Response.Redirect("/Account");
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            
        }
    }
}