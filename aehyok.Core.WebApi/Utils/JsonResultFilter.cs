using aehyok.Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace aehyok.Core.WebApi.Utils
{
    public class JsonResultFilter : IAsyncAlwaysRunResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is FileResult ||
                context.Result is JsonResult || context.Result is JsonResultModel)
            {
                await next();
                return;
            }

            var jsonResult = new JsonResultModel();
            jsonResult.Code = 200;
            jsonResult.Message = "操作成功";
            jsonResult.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            if (context.Result is ObjectResult objectResult)
            {
                if (objectResult == null)
                {
                    jsonResult.Data = null;
                }
                else if (objectResult.Value is IPagedList pagedList)
                {
                    jsonResult.Data = new PagedJsonResultModel(pagedList);
                }
                else
                {
                    jsonResult.Data = objectResult.Value;
                }
            }
            else if (context.Result is ContentResult contentResult)
            {
                jsonResult.Data = contentResult.Content;
            }
            else if (context.Result is StatusCodeResult statusCodeResult)
            {
                jsonResult.Data = null;
            }

            context.Result = new JsonResult(jsonResult);

            await next();
        }
    }
}
