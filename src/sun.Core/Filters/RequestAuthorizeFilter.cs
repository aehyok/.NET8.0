using sun.Core.Services;
using sun.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace sun.Core.Filters
{
    /// <summary>
    /// 请求接口权限过滤器而AuthenticationHandler则是用户认证，token认证
    /// </summary>
    public class RequestAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly IPermissionService permissionService;

        public RequestAuthorizeFilter(IPermissionService permissionService)
        {
            this.permissionService = permissionService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // 接口标记了[AllowAnonymous]，则不需要进行权限验证
            if (context.ActionDescriptor.EndpointMetadata.Any(a => a.GetType() == typeof(AllowAnonymousAttribute)))
            {
                return;
            }

            // 其他需要登录验证的，则通过AuthenticationHandler进行用户认证
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RequestJsonResult(new RequestResultModel(StatusCodes.Status401Unauthorized, "请先登录", null));
                return;
            }

            if (context.ActionDescriptor is not null && context.ActionDescriptor is ControllerActionDescriptor descriptor)
            {
                var namespaceStr = descriptor.ControllerTypeInfo.Namespace;
                var controllerName = descriptor.ControllerName;
                var actionName = descriptor.ActionName;

                var code = $"{namespaceStr}.{controllerName}.{actionName}";

                var hasPermission = true;  // await this.permissionService.CheckUserResourcePermissionAsync(code);
                if (hasPermission)
                {
                    return;
                }

                context.Result = new RequestJsonResult(new RequestResultModel(StatusCodes.Status403Forbidden, "暂无权限", null));
                await Task.CompletedTask;
            }
        }
    }
}
