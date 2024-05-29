using sun.Basic.Domains;
using sun.Basic.Dtos;
using sun.Basic.Dtos.Create;
using sun.Basic.Dtos.Query;
using sun.Basic.Services;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure.Exceptions;
using sun.Infrastructure.Models;
using sun.Infrastructure.Utils;
using Ardalis.Specification;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace sun.Basic.Api.Controllers
{
    /// <summary>
    /// 字典管理
    /// </summary>
    public class DictionaryController(
        IDictionaryGroupService dictionaryGroupService,
        IDictionaryItemService dictionaryItemService) : BasicControllerBase
    {

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

            return await dictionaryGroupService.GetPagedListAsync<DictionaryGroupDto>(spec, model.Page, model.Limit);
        }

        /// <summary>
        /// 获取分组详情
        /// </summary>
        /// <param name="id">分组Id</param>
        /// <returns></returns>
        [HttpGet("group/{id}")]
        public Task<DictionaryGroupDto> GetGroupByIdAsync(long id)
        {
            return dictionaryGroupService.GetAsync<DictionaryGroupDto>(a => a.Id == id);
        }

        /// <summary>
        /// 添加分组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("group")]
        public async Task<long> PostGroupAsync(CreateDictionaryGroupDto model)
        {
            var entity = this.Mapper.Map<DictionaryGroup>(model);
            await dictionaryGroupService.InsertAsync(entity);
            return entity.Id;
        }

        /// <summary>
        /// 修改分组信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("group/{id}")]
        public async Task<StatusCodeResult> PutGroupAsync(long id, CreateDictionaryGroupDto model)
        {
            var entity = await dictionaryGroupService.GetAsync(a => a.Id == id) ?? throw new Exception("你要修改的数据不存在");

            // 修改分组的时候禁止修改分组 Code
            model.Code = entity.Code;

            entity = this.Mapper.Map(model, entity);

            await dictionaryGroupService.UpdateAsync(entity);

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
            var entity = await dictionaryGroupService.GetByIdAsync(id) ?? throw new ErrorCodeException(-1, "你要删除的数据不存在");
            await dictionaryGroupService.DeleteAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 获取字典项列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<DictionaryItemDto>> GetItemAsync([FromQuery] DictionaryItemQueryDto model)
        {
            if (model.DictionaryGroupId <= 0 && model.GroupCode.IsNullOrEmpty())
            {
                throw new ErrorCodeException(-1, "分组id 和 分组Key 至少选填一项");
            }

            if (model.DictionaryGroupId == 0)
            {
                var group = await dictionaryGroupService.GetAsync(a => a.Code == model.GroupCode);
                if (group is null)
                {
                    throw new ErrorCodeException(-1, $"根据字典分组key [{model.GroupCode}] 未查询到任何分组信息");
                }

                model.DictionaryGroupId = group.Id;
            }

            return await dictionaryItemService.GetTreeListAsync(model.DictionaryGroupId, model.Keyword);
        }

        /// <summary>
        /// 获取字典项详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<DictionaryItemDto> GetItemByIdAsync(long id)
        {
            return await dictionaryItemService.GetAsync<DictionaryItemDto>(a => a.Id == id);
        }

        /// <summary>
        /// 添加字典项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> PostItemAsync(CreateDictionaryItemDto model)
        {
            var entity = this.Mapper.Map<DictionaryItem>(model);
            if (model.DictionaryGroupId <= 0)
            {
                var dictionaryGroup = await dictionaryGroupService.GetAsync(a => a.Code == model.DictionaryGroupCode);
                if (dictionaryGroup is null)
                {
                    throw new ErrorCodeException(-1, "字典分组未找到");
                }

                entity.DictionaryGroupId = dictionaryGroup.Id;
            }

            await dictionaryItemService.InsertAsync(entity);
            return entity.Id;
        }

        /// <summary>
        /// 修改字典项
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<StatusCodeResult> PutItemAsync(long id, CreateDictionaryItemDto model)
        {
            var entity = await dictionaryItemService.GetAsync(a => a.Id == id);
            if (entity is null)
            {
                throw new Exception("你要修改的数据不存在");
            }

            if (model.DictionaryGroupId <= 0)
            {
                var dictionaryGroup = await dictionaryGroupService.GetAsync(a => a.Code == model.DictionaryGroupCode);
                if (dictionaryGroup is null)
                {
                    throw new ErrorCodeException(-1, "字典分组未找到");
                }

                model.DictionaryGroupId = dictionaryGroup.Id;
            }

            entity = this.Mapper.Map(model, entity);

            await dictionaryItemService.UpdateAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 删除字典项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<StatusCodeResult> DeleteItemAsync(long id)
        {
            var entity = await dictionaryItemService.GetByIdAsync(id);

            if (entity is null)
            {
                throw new ErrorCodeException(-1, "您要删除的数据不存在");
            }

            await dictionaryItemService.DeleteAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 启用字典项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("Enable/{id}")]
        public async Task<StatusCodeResult> ItemEnableAsync(long id)
        {
            var entity = await dictionaryItemService.GetByIdAsync(id);

            if (entity is null)
            {
                throw new ErrorCodeException(-1, "您要启用的数据不存在");
            }

            entity.IsEnable = true;

            await dictionaryItemService.UpdateAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 禁用字典项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("Disable/{id}")]
        public async Task<StatusCodeResult> ItemDisableAsync(long id)
        {
            var entity = await dictionaryItemService.GetByIdAsync(id);

            if (entity is null)
            {
                throw new ErrorCodeException(-1, "您要启用的数据不存在");
            }

            entity.IsEnable = false;

            await dictionaryItemService.UpdateAsync(entity);

            return Ok();
        }
    }
}
