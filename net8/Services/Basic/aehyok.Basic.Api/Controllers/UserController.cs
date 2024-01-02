using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
using aehyok.Basic.Dtos.Query;
using aehyok.Basic.Services;
using aehyok.EntityFramework.Repository;
using aehyok.Infrastructure.Exceptions;
using Ardalis.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using aehyok.Infrastructure;

namespace aehyok.Basic.Api.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserController(IUserService userService) : BasicControllerBase
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="roleService"></param>
        /// <param name="regionService"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IPagedList<UserDto>> GetListAsync([FromQuery] UserQueryModel model
            , [FromServices] IRoleService roleService
            , [FromServices] IRegionService regionService)
        {
            var spec = Specifications<User>.Create();
            spec.Query.OrderByDescending(a => a.Id).Include(a => a.Roles);

            if (!string.IsNullOrWhiteSpace(model.Keyword))
            {
                spec.Query.Search(a => a.UserName, $"%{model.Keyword}%")
                          .Search(a => a.Mobile, $"%{model.Keyword}%")
                          .Search(a => a.NickName, $"%{model.Keyword}%")
                          .Search(a => a.RealName, $"%{model.Keyword}%");
            }

            if (model.IsEnable.HasValue)
            {
                spec.Query.Where(a => a.IsEnable == model.IsEnable.Value);
            }

            if (model.RoleId.HasValue && model.RoleId != 0)
            {
                spec.Query.Where(a => a.UserRoles.Any(c => c.RoleId == model.RoleId.Value));
            }

            if (model.RegionCode.IsNotNullOrEmpty())
            {
                var regionInfo = await regionService.GetAsync(e => e.Code == model.RegionCode);
                if (regionInfo is null) throw new ErrorCodeException(-1, "未找到对应区域");
                model.RegionId = regionInfo.Id;
            }

            
            // 所有包含农户角色和非游客角色之外角色信息的的用户
            if (model.RegionId.HasValue && model.RegionId != 0)
            {
                if (model.IncludeChilds)
                {
                    spec.Query.Where(a => a.UserRoles.Any(c => EF.Functions.Like(c.Region.IdSequences, $"%.{model.RegionId.Value}.%")));
                }
                else
                {
                    spec.Query.Where(a => a.UserRoles.Any(c => c.RegionId == model.RegionId.Value));
                }
            }

            return await userService.GetPagedListAsync<UserDto>(spec, model.Page, model.Limit);
        }
    }
}
