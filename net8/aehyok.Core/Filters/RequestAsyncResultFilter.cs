using aehyok.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using X.PagedList;

namespace aehyok.Core.Filters
{
    /// <summary>
    /// 异步请求结果过滤器
    /// </summary>
    public class RequestAsyncResultFilter : IAsyncResultFilter
    {
        /// <summary>
        /// 在返回结果之前调用，用于统一返回数据格式
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (Activity.Current is not null)
            {
                context.HttpContext.Response.Headers.Append("X-TraceId", Activity.Current?.TraceId.ToString());
            }

            if(context.Result is BadRequestObjectResult badRequestObjectResult)
            {
                var resultModel = new RequestResultModel
                {
                    Code = badRequestObjectResult.StatusCode ?? StatusCodes.Status400BadRequest,
                    Message = "请求参数验证错误",
                    Data = badRequestObjectResult.Value
                };

                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new RequestJsonResult(resultModel);
            }
            // 比如直接return Ok();
            else if(context.Result is StatusCodeResult statusCodeResult)
            {
                var resultModel = new RequestResultModel
                {
                    Code = statusCodeResult.StatusCode,
                    Message = statusCodeResult.StatusCode == 200 ? "Success" : "请求发生错误",
                    Data = statusCodeResult.StatusCode == 200
                };

                context.Result = new RequestJsonResult(resultModel);
            }
            else if(context.Result is ObjectResult result)
            {
                if(result.Value is null)
                {
                    var resultModel = new RequestResultModel
                    {
                        Code = result.StatusCode ?? context.HttpContext.Response.StatusCode,
                        Message = "未请求到数据"
                    };
                    context.Result = new RequestJsonResult(resultModel);
                }
                else if(result.Value is not RequestJsonResult)
                {
                    if (result.Value is IPagedList pagedList)
                    {
                        var resultModel = new RequestPagedResultModel
                        {
                            Message = "Success",
                            Data = result.Value,
                            Total = pagedList.TotalItemCount,
                            Page = pagedList.PageNumber,
                            TotalPage = pagedList.PageCount,
                            Limit = pagedList.PageSize,
                            Code = result.StatusCode ?? context.HttpContext.Response.StatusCode
                        };

                        context.Result = new RequestJsonResult(resultModel);
                    }
                    else
                    {
                        var resultModel = new RequestResultModel
                        {
                            Code = result.StatusCode ?? context.HttpContext.Response.StatusCode,
                            Message = "Success",
                            Data = result.Value
                        };

                        context.Result = new RequestJsonResult(resultModel);
                    }
                }
            }

            await next();
        }
    }
}
