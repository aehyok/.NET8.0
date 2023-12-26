using aehyok.Infrastructure.Exceptions;
using aehyok.Infrastructure.Models;
using aehyok.Infrastructure.Options;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace aehyok.Infrastructure.Filters
{
    /// <summary>
    /// 错误异常处理过滤器（控制器构造函数、执行Action接口方法、执行ResultFilter结果过滤器）
    /// </summary>
    public class ApiAsyncExceptionFilter(ILogger<ApiAsyncExceptionFilter> logger) : IAsyncExceptionFilter
    {
        private readonly ILogger<ApiAsyncExceptionFilter> logger = logger;

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;

            //设置错误返回结果
            var resultModel = new RequestResultModel();
            if(exception is ErrorCodeException errorCodeException)
            {
                resultModel.Code = errorCodeException.ErrorCode;
            }
            else
            {
                resultModel.Code = (int)HttpStatusCode.InternalServerError;
            }

            resultModel.Message = exception.Message;

            // 读取配置文件中是否配置了显示堆栈信息
            if(App.Options<CommonOptions>().ShowStackTrace)
            {
                resultModel.Data = exception.StackTrace;
            }

            context.Result = new RequestJsonResult(resultModel);

            //用来指示错误异常已处理
            context.ExceptionHandled = true;

            //所有接口如果包含异常，都返回500
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var message = exception.Message;

            logger.LogError(exception, message);
            await Task.CompletedTask;
        }
    }
}
