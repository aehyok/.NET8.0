using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
using aehyok.Basic.Dtos.Query;
using aehyok.Basic.Services;
using aehyok.Core;
using aehyok.EntityFramework.Repository;
using Ardalis.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace aehyok.Basic.Api.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleController(IRoleService roleService) : BasicControllerBase
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
    }
}
