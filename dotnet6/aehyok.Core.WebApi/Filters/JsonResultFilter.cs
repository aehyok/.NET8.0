using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using aehyok.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace aehyok.Base.Filters
{
    public class JsonResultFilter : IAsyncAlwaysRunResultFilter
    {
        private readonly ILogger logger;

        public JsonResultFilter(ILogger<JsonResultFilter> logger)
        {
            this.logger = logger;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is FileResult ||
                context.Result is JsonResult)
            {
                await next();
                return;
            }

            var resultModel = new JsonResultModel();
            resultModel.Code = 200;
            resultModel.Message = "操作成功";
            resultModel.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            if (context.Result is ObjectResult objectResult)
            {
                if (objectResult == null)
                {
                    resultModel.Data = null;
                }
                else if (objectResult.Value is IPagedList pagedList)
                {
                    resultModel.Data = new PagedJsonResultModel(pagedList);
                }
                else
                {
                    resultModel.Data = objectResult.Value;
                }
            }
            else if (context.Result is ContentResult contentResult)
            {
                resultModel.Data = contentResult.Content;
            }
            else if (context.Result is StatusCodeResult statusCodeResult)
            {
                resultModel.Data = null;
            }

            context.Result = new JsonResult(resultModel);

            await next();
        }
    }
}
