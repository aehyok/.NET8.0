using aehyok.Basic.Domains;
using aehyok.Core;
using aehyok.Core.Dtos;
using aehyok.Core.Dtos.Query;
using aehyok.Core.Services;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace aehyok.Basic.Api.Controllers
{
    /// <summary>
    /// 登录日志 
    /// </summary>
    public class TokenLogController(IUserTokenService userTokenService, IUserService userService, IUserRoleService userRoleService) : ApiControllerBase
    {
        /// <summary>
        /// 获取登录日志列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("log/list")]
        public async Task<IPagedList<UserTokenLogDto>> GetLogListAsync([FromQuery] UserTokenQueryDto model)
        {
            var filter = PredicateBuilder.New<UserToken>(true);

            if (model.platformType > 0)
            {
                filter.And(a => a.PlatformType == model.platformType);
            }

            var query = from ut in userTokenService.GetExpandable().Where(filter)
                        join u in userService.GetQueryable() on ut.UserId equals u.Id
                        join ur in userRoleService.GetQueryable() on ut.RoleId equals ur.RoleId
                        select new UserTokenLogDto
                        {
                            Id = ut.Id,
                            loginUser = u.RealName,
                            loginAt = ut.CreatedAt,
                            PlatformType = ut.PlatformType,
                            RoleName = ur.Role.Name,
                            RegionName = ur.Region.Name
                        };

            query.OrderByDescending(a => a.Id);

            return await query.ToPagedListAsync(model.Page, model.Limit);
        }

    }
}
