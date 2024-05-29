using Ardalis.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sun.Basic.Services;
using sun.Core.Domains;
using sun.Core.Dtos;
using sun.Core.Services;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure.Exceptions;
using sun.Infrastructure.Models;
using X.PagedList;

namespace sun.NCDP.Api.Controllers
{
    /// <summary>
    /// 工作流定义
    /// </summary>

    public class WorkFlowDefineController(IWorkFlowDefineService workFlowDefineService) : NCDPControllerBase
    {

        /// <summary>
        /// 获取工作流定义列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IPagedList<WorkFlowDefineDto>> GetDefineListAsync([FromQuery] PagedQueryModelBase model)
        {
            var spec = Specifications<WorkFlowDefine>.Create();

            spec.Query.OrderBy(a => a.DisplayOrder);
            if(!string.IsNullOrWhiteSpace(model.Keyword))
            {
                spec.Query.Search(a => a.FlowName, $"%{model.Keyword}%");
            }
            return await workFlowDefineService.GetPagedListAsync<WorkFlowDefineDto>(spec, model.Page, model.Limit);
        }

        /// <summary>
        /// 获取工作流定义详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<WorkFlowDefineDto> GetDefineByIdAsync(long id)
        {
            return await workFlowDefineService.GetAsync<WorkFlowDefineDto>(a => a.Id == id);
        }

        /// <summary>
        /// 添加工作流定义
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> PostDefineAsync(CreateWorkFlowDefineDto model)
        {
            var entity = this.Mapper.Map<WorkFlowDefine>(model);
            await workFlowDefineService.InsertAsync(entity);
            return entity.Id;
        }

        /// <summary>
        /// 修改工作流定义
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<StatusCodeResult> PutDefineAsync(long id, CreateWorkFlowDefineDto model)
        {
            var entity = await workFlowDefineService.GetAsync(a => a.Id == id) ?? throw new Exception("修改的数据不存在");

            //model.Code = entity.Code;  // 不允许修改Code

            entity = this.Mapper.Map(model, entity);

            await workFlowDefineService.UpdateAsync(entity);
            return Ok();
        }

        /// <summary>
        /// 删除工作流定义
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<StatusCodeResult> DeleteDefineAsync(long id)
        {
            await workFlowDefineService.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// 启用工作流定义
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ErrorCodeException"></exception>
        [HttpPut("Enable/{id}")]
        public async Task<StatusCodeResult> EnableDefine(long id)
        {
            var entity = await workFlowDefineService.GetByIdAsync(id);

            if (entity is null)
            {
                throw new ErrorCodeException(-1, "启用的数据不存在");
            }
            entity.IsEnable = true;

            await workFlowDefineService.UpdateAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 禁用工作流定义
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ErrorCodeException"></exception>
        [HttpPut("Disable/{id}")]
        public async Task<StatusCodeResult> DisableDefine(long id)
        {
            var entity = await workFlowDefineService.GetByIdAsync(id);

            if (entity is null)
            {
                throw new ErrorCodeException(-1, "禁用的数据不存在");
            }
            entity.IsEnable = true;

            await workFlowDefineService.UpdateAsync(entity);

            return Ok();
        }
    }
}
