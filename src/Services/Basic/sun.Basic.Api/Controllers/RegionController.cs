using sun.Basic.Services;
using sun.Core.Domains;
using sun.Core.Dtos;
using sun.Core.Dtos.Create;
using sun.Core.Dtos.Query;
using sun.Core.Services;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure.Exceptions;
using Ardalis.Specification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using sun.Infrastructure.Enums;

namespace sun.Basic.Api.Controllers
{
    /// <summary>
    /// 行政区域管理
    /// </summary>
    //[Authorize(Roles = SystemRoles.ROOT)]
    public class RegionController(IRegionService regionService, IAsyncTaskService asyncTaskService) : BasicControllerBase
    {
        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<List<RegionDto>> GetListAsync([FromQuery] RegionQueryDto model)
        {
            var spec = Specifications<Region>.Create();
            spec.Query.OrderBy(a => a.Order);

            if (!model.Keyword.IsNullOrEmpty())
            {
                spec.Query.Search(a => a.Name, $"%{model.Keyword}%")
                          .Search(a => a.ShortName, $"%{model.Keyword}%")
                          .Search(a => a.Code, $"%{model.Keyword}%");
            }

            if (model.ParentId == 0 && model.IsCurrent)
                spec.Query.Where(a => a.Id == this.CurrentUser.RegionId);
            else
                spec.Query.Where(a => a.ParentId == model.ParentId);

            return regionService.GetListAsync<RegionDto>(spec);
        }

        /// <summary>
        /// 添加行政区划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> PostAsync(CreateRegionDto model)
        {
            var entity = this.Mapper.Map<Region>(model);

            //if (entity.ParentId == 0)
            //{
            //    throw new ErrorCodeException(-1, "禁止创建根节点区域");
            //}

            await regionService.InsertAsync(entity);
            return entity.Id;
        }

        /// <summary>
        /// 修改行政区域
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPut("{id}")]
        public async Task<StatusCodeResult> PutAsync(long id, CreateRegionDto model)
        {
            var entity = await regionService.GetAsync(a => a.Id == id);
            if (entity is null)
            {
                throw new ErrorCodeException(-1, "你要修改的数据不存在");
            }

            entity = this.Mapper.Map(model, entity);

            await regionService.UpdateAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 获取区域信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<RegionDto> GetByIdAsync(long id)
        {
            return await regionService.GetAsync<RegionDto>(a => a.Id == id);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ErrorCodeException"></exception>
        [HttpDelete("{id}")]
        public async Task<StatusCodeResult> DeleteAsync(long id)
        {
            var entity = await regionService.GetByIdAsync(id);
            if (entity is null)
            {
                throw new ErrorCodeException(-1, "你要删除的数据不存在");
            }

            await regionService.DeleteAsync(entity);
            return Ok();
        }

        /// <summary>
        /// 区域导出
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("export")]
        public async Task<AsyncTaskDto> ExportAsync(RegionExportQueryDto model)
        {
            var asyncTask = await asyncTaskService.GenerateTaskAsync(model, "RegionExport");

            return this.Mapper.Map<AsyncTaskDto>(asyncTask);
        }

        /// <summary>
        /// 启用该区域
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("Enable/{id}")]
        public async Task<StatusCodeResult> ItemEnableAsync(long id)
        {
            var entity = await regionService.GetByIdAsync(id);

            if (entity is null)
            {
                throw new ErrorCodeException(-1, "您要启用的数据不存在");
            }

            entity.IsEnable = true;

            await regionService.UpdateAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 禁用该区域
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("Disable/{id}")]
        public async Task<StatusCodeResult> ItemDisableAsync(long id)
        {
            var entity = await regionService.GetByIdAsync(id);

            if (entity is null)
            {
                throw new ErrorCodeException(-1, "您要禁用的数据不存在");
            }

            entity.IsEnable = false;

            await regionService.UpdateAsync(entity);

            return Ok();
        }
    }
}
