using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
using aehyok.Basic.Dtos.Create;
using aehyok.Basic.Dtos.Query;
using aehyok.Basic.Services;
using aehyok.Core;
using aehyok.EntityFramework.Repository;
using aehyok.Infrastructure.Exceptions;
using Ardalis.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace aehyok.Basic.Api.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleController(IRoleService roleService, IPermissionService permissionService) : BasicControllerBase
    {
        // <summary>
        /// 获取角色分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<IPagedList<RoleDto>> GetAsync([FromQuery] RoleQueryModel model)
        {
            var spec = Specifications<Role>.Create();
            spec.Query.OrderBy(a => a.Order);

            if (!string.IsNullOrEmpty(model.Keyword))
            {
                spec.Query.Search(a => a.Name, $"%{model.Keyword}%").Search(a => a.Code, $"%{model.Keyword}%").Search(a => a.Remark, $"%{model.Keyword}%");
            }

            if (model.IsEnable.HasValue)
            {
                spec.Query.Where(a => a.IsEnable == model.IsEnable.Value);
            }

            return roleService.GetPagedListAsync<RoleDto>(spec, model.Page, model.Limit);
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<RoleDto> GetByIdAsync(long id)
        {
            return roleService.GetByIdAsync<RoleDto>(id);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> PostAsync(CreateRoleModel model)
        {
            var entity = this.Mapper.Map<Role>(model);
            await roleService.InsertAsync(entity);
            return entity.Id;
        }

        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<StatusCodeResult> PutAsync(long id, CreateRoleModel model)
        {
            var entity = await roleService.GetAsync(a => a.Id == id);
            if (entity is null)
            {
                throw new ErrorCodeException(-1, "你要修改的数据不存在");
            }

            entity = this.Mapper.Map(model, entity);

            await roleService.UpdateAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 启用角色
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPut("enable/{id}")]
        public async Task<StatusCodeResult> EnableAsync(long id)
        {
            var entity = await roleService.GetAsync(a => a.Id == id);
            if (entity is null)
            {
                throw new ErrorCodeException(-1, "你要启用的数据不存在");
            }

            entity.IsEnable = true;
            await roleService.UpdateAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 禁用角色
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPut("disable/{id}")]
        public async Task<StatusCodeResult> DisableAsync(long id)
        {
            var entity = await roleService.GetAsync(a => a.Id == id);
            if (entity is null)
            {
                throw new ErrorCodeException(-1, "你要禁用的数据不存在");
            }

            entity.IsEnable = false;
            await roleService.UpdateAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpDelete("{id}")]
        public async Task<StatusCodeResult> DeleteAsync(long id)
        {
            var entity = await roleService.GetAsync(a => a.Id == id);
            if (entity is null)
            {
                throw new ErrorCodeException(-1, "你要删除的数据不存在");
            }

            if (entity.IsSystem)
            {
                throw new ErrorCodeException(-1, "禁止删除系统内置角色");
            }

            await roleService.DeleteAsync(entity);
            return Ok();
        }

        /// <summary>
        /// 获取对象权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<List<PermissionDto>> GetRoleAsync([FromQuery] PermissionQueryModel model)
        {
            return permissionService.GetRolePermissionAsync(model.RoleId);
        }
    }
}
