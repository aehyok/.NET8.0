using aehyok.Core.Log;
using aehyok.Core.Services;
using aehyok.Infrastructure;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace aehyok.Core.Filters
{
    /// <summary>
    /// 日志记录过滤器
    /// </summary>
    public class LogActionFilter(IOperationLogService operationLogService) : IAsyncActionFilter
    {
        /// <summary>
        /// 接口方法执行后
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (context.HttpContext.Request.Headers.ContainsKey("menuCode") && !string.IsNullOrEmpty(context.HttpContext.Request.Headers["menuCode"]))
            {
                var menuCode = context.HttpContext.Request.Headers["menuCode"].ToString();
                if (actionDescriptor != null)
                {
                    var logAttribute = actionDescriptor.MethodInfo.GetCustomAttribute<LogActionAttribute>();

                    if (logAttribute != null)
                    {
                        string logMessage = logAttribute.MessageTemplate;

                        //context.HttpContext.Request.

                        IList<ParameterDescriptor> dynamicFieldValue = actionDescriptor.Parameters;

                        await operationLogService.LogAsync(menuCode, logMessage);
                    }
                }
            }
        }
    }
}
