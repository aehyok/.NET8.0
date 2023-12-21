using aehyok.Basic.Domains;
using aehyok.Basic.Services;
using aehyok.EntityFramework.Repository;
using aehyok.Infrastructure.Models;
using Ardalis.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace aehyok.Basic.Api.Controllers
{

    /// <summary>
    /// 字典管理
    /// </summary>
    public class DictionaryController : BasicControllerBase
    {
        private readonly  ILogger<DictionaryController> logger;
        private readonly IDictionaryGroupService dictionaryGroupService;
        private readonly IDictionaryItemService dictionaryItemService;

        public DictionaryController(
            ILogger<DictionaryController> logger,
            IDictionaryGroupService dictionaryGroupService,
            IDictionaryItemService dictionaryItemService)
        {
            this.logger = logger;
            this.dictionaryGroupService = dictionaryGroupService;
            this.dictionaryItemService = dictionaryItemService;
        }

        /// <summary>
        /// 获取字典分组
        /// </summary>
        /// <param name="model">字典分组模型</param>
        /// <returns></returns>
        [HttpGet("group")]
        public async Task<IPagedList<DictionaryGroupDto>> GetGroupAsync([FromQuery] PagedQueryModelBase model)
        {
            var spec = Specifications<DictionaryGroup>.Create();

            spec.Query.OrderBy(a => a.Order);

            if (!string.IsNullOrWhiteSpace(model.Keyword))
            {
                spec.Query.Search(a => a.Name, $"%{model.Keyword}%");
            }

            return await this.dictionaryGroupService.GetPagedListAsync<DictionaryGroupDto>(spec, model.Page, model.Limit);
        }

        /// <summary>
        /// 获取分组详情
        /// </summary>
        /// <param name="id">分组Id</param>
        /// <returns></returns>
        [HttpGet("group/{id}")]
        public Task<DictionaryGroupDto> GetGroupByIdAsync(long id)
        {
            return this.dictionaryGroupService.GetAsync<DictionaryGroupDto>(a => a.Id == id);
        }
    }
}
