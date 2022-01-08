using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace aehyok.Core.WebApi.Utils
{
    /// <summary>
    /// 控制器构造函数之后，接口方法调用之前
    /// </summary>
    public class AuthorizationFilter : IAsyncActionFilter
    {

        //private readonly IEncryptionService encryptionService;
        public AuthorizationFilter()
        {
            //this.encryptionService = encryptionService;

        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            //如果用户方法的Action带有AllowAnonymousAttribute，则不进行授权验证
            ControllerActionDescriptor descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            AllowAnonymousAttribute allowAnonymousAttribute = descriptor.MethodInfo.GetCustomAttribute<AllowAnonymousAttribute>(false);
            if (allowAnonymousAttribute != null)
            {
                await next();
                return;
            }
            else
            {
                string token = context.HttpContext.Request.Headers["Authorization"];
                if (string.IsNullOrWhiteSpace(token))
                {
                    //throw new ValidException("无效的Token!");
                    token = context.HttpContext.Request.Headers["Token"];
                }
                //if (!string.IsNullOrWhiteSpace(token))
                //{
                //    var tokenHash = this.encryptionService.CreateHash(token, "SHA1");

                //    var userToken = await RedisHelper.GetAsync($"token_{tokenHash}");

                //    if (token == userToken)
                //    {
                //        await RedisHelper.SetAsync($"token_{tokenHash}", token, 60 * 60 * 24);
                //    }
                //    else
                //    {
                //        throw new ValidException("Token验证失败!");
                //    }
                //}
                else
                {
                    throw new ValidException("无效的Token!");
                }

                await next();
            }
        }
    }
}
