using sun.Core.Domains;
using sun.Core.Dtos;
using sun.Core.Dtos.Query;
using sun.Core.Services;
using sun.Infrastructure.Models;
using sun.Infrastructure.Utils;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace sun.Basic.Api.Controllers
{

    /// <summary>
    /// 模版管理
    /// </summary>
    public class TemplateController(ITemplateService templateService) : BasicControllerBase
    {
        /// <summary>
        /// 模版列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IPagedList<TemplateDto>> GetListAsync([FromQuery] TemplateQueryDto model)
        {
            var filter = PredicateBuilder.New<Template>(true);

            if (model.Keyword.IsNotNullOrEmpty())
            {
                filter = filter.And(a => a.Name.Contains(model.Keyword) || a.Remark.Contains(model.Keyword));
            }

            return await templateService.GetPagedListAsync<TemplateDto>(filter, model.Page, model.Limit);
        }
    }
}
