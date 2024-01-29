using aehyok.Core.Domains;
using aehyok.Core.Dtos;
using aehyok.Core.Dtos.Query;
using aehyok.Core.Services;
using aehyok.Infrastructure.Models;
using aehyok.Infrastructure.Utils;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace aehyok.Basic.Api.Controllers
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
