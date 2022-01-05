using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using aehyok.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace aehyok.Base.Filters
{
    public class VisualizationResultAttribute : TypeFilterAttribute
    {
        public VisualizationResultAttribute()
            : base(typeof(VisualizationResultFilter))
        { }

        public class VisualizationResultFilter : IAsyncExceptionFilter
        {
            private readonly ILogger logger;

            public VisualizationResultFilter(ILogger<VisualizationResultFilter> logger)
            {
                this.logger = logger;
            }

            public async Task OnExceptionAsync(ExceptionContext context)
            {
                var exception = context.Exception;

                if (exception == null)
                {
                    return;
                }
                if (exception is ValidException)
                {
                    ValidException validException = exception as ValidException;
                    var resultModel = new JsonResultModel();
                    resultModel.Code = validException.ExceptionCode;
                    resultModel.Message = validException.Message;
                    resultModel.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    resultModel.Data = null;
                    context.Result = new JsonResult(resultModel);
                    context.ExceptionHandled = true;
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
                else
                {
                    var resultModel = new JsonResultModel();
                    resultModel.Code = (int)HttpStatusCode.InternalServerError;
                    resultModel.Message = exception.Message;
                    resultModel.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    resultModel.Data = exception.StackTrace;
                    this.logger.LogError(exception, exception.Message);
                    context.Result = new JsonResult(resultModel);
                    context.ExceptionHandled = true;
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
                await Task.CompletedTask;
            }

        }
    }
}
