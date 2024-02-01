using sun.Core.Attributes;
using sun.Core.Services;
using sun.Infrastructure;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.JSInterop.Implementation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace sun.Core.Filters
{
    /// <summary>
    /// 日志记录过滤器
    /// </summary>
    public class OperationLogActionFilter(IOperationLogService operationLogService) : IAsyncActionFilter
    {
        /// <summary>
        /// 接口方法执行后
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>

        public  async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (context.HttpContext.Request.Headers.ContainsKey("Menu-Code") && !string.IsNullOrEmpty(context.HttpContext.Request.Headers["Menu-Code"]))
            {
                var menuCode = context.HttpContext.Request.Headers["Menu-Code"].ToString();
                if (actionDescriptor != null)
                {
                    var json = JsonConvert.SerializeObject(context.ActionArguments);

                    var logAttribute = actionDescriptor.MethodInfo.GetCustomAttribute<OperationLogActionAttribute>();
                    string logMessage = null;
                    if (logAttribute != null)
                    {
                        logMessage = logAttribute.MessageTemplate;
                        if(logMessage is not null)
                        {
                            CreateOperationLogContent(json, ref logMessage);
                        } 
                    }
                    else
                    {
                        // 获取 Action 注释
                        var commentsInfo = DocsHelper.GetMethodComments(actionDescriptor.ControllerTypeInfo.Assembly.GetName().Name, actionDescriptor.MethodInfo);
                        logMessage = commentsInfo;
                    }
                    await operationLogService.LogAsync(menuCode, logMessage, json);
                }
            }
            await next();
        }

        /// <summary>
        /// 根据OperationLogActionAttribute和参数生成操作日志的内容
        /// </summary>
        /// <param name="json"></param>
        /// <param name="logMessage"></param>
        private void CreateOperationLogContent(string json, ref  string logMessage)
        {
            JObject jObject = JObject.Parse(json);

            // 正则提取参数
            Regex regex = new Regex(@"\{([\w\.]+)\}");
            MatchCollection matches = regex.Matches(logMessage);

            // 替换
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Groups[1].Value);
                var key = match.Groups[1].Value;
                logMessage = logMessage.Replace($"{{{key}}}", jObject.SelectToken(key).Value<string>());
            }
        }
    }
}
