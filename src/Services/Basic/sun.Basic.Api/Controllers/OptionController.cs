using sun.Basic.Domains;
using sun.Basic.Dtos;
using sun.Basic.Services;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure.Models;
using sun.Infrastructure.Utils;
using Ardalis.Specification;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using X.PagedList;

namespace sun.Basic.Api.Controllers
{
    /// <summary>
    /// 系统参数
    /// </summary>
    public class OptionController(IOptionsSerivce optionsService) : BasicControllerBase
    {
        /// <summary>
        /// 获取配置列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IPagedList<OptionsDto>> GetListAsync([FromQuery] PagedQueryModelBase model)
        {
            var filter = PredicateBuilder.New<Options>(true);

            if(model.Keyword.IsNotNullOrEmpty())
            {
                filter = filter.And(a => a.Key.Contains(model.Keyword) || a.Remark.Contains(model.Keyword) || a.Value.Contains(model.Keyword));
            }

            return await optionsService.GetPagedListAsync<OptionsDto>(filter, model.Page, model.Limit);

            //var spec = Specifications<Options>.Create();
            //spec.Query.OrderBy(a => a.Id);

            //if (!model.Keyword.IsNullOrEmpty())
            //{
            //    spec.Query.Search(a => a.Key, $"%{model.Keyword}%")
            //        .Search(a => a.Remark, $"%{model.Keyword}%")
            //        .Search(a => a.Value, $"%{model.Keyword}%");
            //}

            //return optionsService.GetPagedListAsync<OptionsDto>(spec, model.Page, model.Limit);
        }
    }
}
