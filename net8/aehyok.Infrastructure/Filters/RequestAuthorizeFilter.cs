using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure.Filters
{ }
//{
//    /// <summary>
//    /// 还需要用户、菜单权限等等的处理
//    /// </summary>
//    public class RequestAuthorizeFilter : IAsyncAuthorizationFilter
//    {
//        private readonly IPermissionService permissionService;

//        public DvsAuthorizeFilter(IPermissionService permissionService)
//        {
//            this.permissionService = permissionService;
//        }

//        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
//        {
//            if (context.ActionDescriptor.EndpointMetadata.Any(a => a.GetType() == typeof(AllowAnonymousAttribute)))
//            {
//                return;
//            }

//            if (!context.HttpContext.User.Identity.IsAuthenticated)
//            {
//                context.Result = new DvsResult(new DvsResultModel(StatusCodes.Status401Unauthorized, "请先登录", null));
//                return;
//            }

//            if (context.ActionDescriptor is not null && context.ActionDescriptor is ControllerActionDescriptor descriptor)
//            {
//                var namespaceStr = descriptor.ControllerTypeInfo.Namespace;
//                var controllerName = descriptor.ControllerName;
//                var actionName = descriptor.ActionName;

//                var code = $"{namespaceStr}.{controllerName}.{actionName}";

//                var hasPermission = await this.permissionService.CheckUserResourcePermissionAsync(code);
//                if (hasPermission)
//                {
//                    return;
//                }

//                context.Result = new DvsResult(new DvsResultModel(StatusCodes.Status403Forbidden, "暂无权限", null));
//            }
//        }
//    }
//}
