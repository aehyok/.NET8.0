using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
using aehyok.Basic.Services;
using aehyok.EntityFramework.Repository;
using aehyok.Infrastructure.Exceptions;
using aehyok.Infrastructure.Models;
using Ardalis.Specification;
using LinqKit;
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

        /// <summary>
        /// 添加分组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("group")]
        public async Task<long> PostGroupAsync(CreateDictionaryGroupModel model)
        {
            var entity = this.Mapper.Map<DictionaryGroup>(model);
            await this.dictionaryGroupService.InsertAsync(entity);
            return entity.Id;
        }

        /// <summary>
        /// 修改分组信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("group/{id}")]
        public async Task<StatusCodeResult> PutGroupAsync(long id, CreateDictionaryGroupModel model)
        {
            var entity = await this.dictionaryGroupService.GetAsync(a => a.Id == id);
            if (entity is null)
            {
                throw new Exception("你要修改的数据不存在");
            }

            // 修改分组的时候禁止修改分组 Code
            model.Code = entity.Code;

            entity = this.Mapper.Map(model, entity);

            await this.dictionaryGroupService.UpdateAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("group/{id}")]  
        public async Task<StatusCodeResult> DeleteGroupAsync(long id)
        {
            var entity = await this.dictionaryGroupService.GetByIdAsync(id);
            if (entity is null)
            {
                throw new ErrorCodeException(-1, "你要删除的数据不存在");
            }

            await this.dictionaryGroupService.DeleteAsync(entity);

            return Ok();
        }
    }
}
