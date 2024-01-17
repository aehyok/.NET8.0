using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
using aehyok.Basic.Services;
using aehyok.Core;
using aehyok.Core.Domains;
using aehyok.Core.Dtos;
using aehyok.Core.Dtos.Query;
using aehyok.Core.Services;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure.Enums;
using aehyok.Infrastructure.Exceptions;
using aehyok.Infrastructure.Utils;
using aehyok.Redis;
using Ardalis.Specification;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using X.PagedList;
using StringExtensions = aehyok.Infrastructure.Utils.StringExtensions;

namespace aehyok.Basic.Api.Controllers
{

    /// <summary>
    /// Token 管理
    /// </summary>
    public class TokenController(IUserTokenService userTokenService, 
        IUserRoleService userRoleService,
        IUserService userService, 
        IRedisService redisService, 
        IPermissionService permissionService,
        IMenuService menuService,
        IRegionService regionService) : BasicControllerBase
    {

        /// <summary>
        /// 获取图片验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("captcha")]
        [AllowAnonymous]
        public Task<CaptchaDto> GetCaptchaAsync()
        {
            return userTokenService.GenerateCaptchaAsync();
        }

        /// <summary>
        /// 账号密码登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("password")]
        [AllowAnonymous]
        public async Task<UserTokenDto> PostAsync(PasswordLoginDto model)
        {
            if (!await userTokenService.ValidateCaptchaAsync(model.Captcha, model.CaptchaKey))
            {
                throw new Exception("验证码错误");
            }

            return await userTokenService.LoginWithPasswordAsync(model.UserName, model.Password, model.PlatformType);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("signout")]
        public async Task<StatusCodeResult> SignoutAsync()
        {
            if (!this.CurrentUser.IsAuthenticated)
            {
                throw new ErrorCodeException(-1, "请先登录");
            }

            var tokenHash = StringExtensions.EncodeMD5(this.CurrentUser.Token);

            // 修改 UserToken 中的 ExpirationDate 为当前时间
            var userToken = await userTokenService.GetAsync(a => a.TokenHash == tokenHash && a.UserId == this.CurrentUser.UserId);
            if (userToken != null)
            {
                userToken.ExpirationDate = DateTime.Now;
                userToken.LoginType = LoginType.logout;
                await userTokenService.UpdateAsync(userToken);
                // 删除 Redis 中的缓存
                await redisService.DeleteAsync(CoreRedisConstants.UserToken.Format(userToken.TokenHash));
            }

            return Ok();
        }

        /// <summary>
        /// 使用 Refresh Token 获取新的 Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Refresh")]
        [AllowAnonymous]
        public Task<UserTokenDto> RefreshAsync(RefreshTokenDto model)
        {
            return userTokenService.RefreshTokenAsync(model.UserId, model.RefreshToken);
        }

        /// <summary>
        /// 获取当前用户拥有的菜单权限列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("permission")]
        public async Task<List<RolePermissionDto>> GetCurrentUserPermissionAsync(PlatformType platformType)
        {
            var menuFilter = PredicateBuilder.New<Menu>(true);

            menuFilter.And(a => a.PlatformType == platformType);

            var spec = Specifications<Menu>.Create();
            
            var currentUser = this.CurrentUser;

            var query = from p in permissionService.GetQueryable()
                        join m in menuService.GetExpandable().Where(menuFilter) on p.MenuId equals m.Id
                        join ur in userRoleService.GetQueryable() on p.RoleId equals ur.RoleId
                        join r in userService.GetQueryable() on ur.UserId equals r.Id
                        where ur.UserId == currentUser.UserId && ur.RoleId == currentUser.RoleId
                        select new RolePermissionDto
                        {
                            MenuId = m.Id,
                            RoleId = p.RoleId,
                            MenuName = m.Name,
                            MenuCode = m.Code,
                            ParentId = m.ParentId,
                            Order = m.Order
                        };
            var list = await query.ToListAsync();

            List<RolePermissionDto> getChildren(long parentId)
            {
                var children = list.Where(a => a.ParentId == parentId).OrderBy(a => a.Order).ToList();
                return children.Select(a =>
                {
                    a.Children = getChildren(a.MenuId);
                    return a;
                }).ToList();
            }

            return getChildren(0);
        }

        /// <summary>
        /// 获取当前登录用户详细信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("userinfo")]
        public async Task<CurrentUserDto> GetCurrentUserAsync()
        {
            if (!this.CurrentUser.IsAuthenticated)
                return default;

            var user = await userService.GetAsync(
                a => a.Id == this.CurrentUser.UserId, 
                includes: a => a.Include(c => c.UserRoles)
                    .ThenInclude(c => c.Role)
                    .Include(c => c.UserRoles)
                    .ThenInclude(c => c.Region));
                                                                                                             
            var result = this.Mapper.Map<CurrentUserDto>(user);
            //result.Roles = result.Roles.OrderBy(item => item.IsDefault).ToList();

            if (result is null)
                throw new ErrorCodeException(100212, "用户信息不存在");
                
            var regionInfo = await regionService.GetAsync(r => r.Id == this.CurrentUser.RegionId);
            string[] regionFullName = new List<string>().ToArray();
            if (regionInfo is not null)
            {
                var ids = regionInfo.IdSequences.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList().Select(id => id.Long());
                var regions = (await regionService.GetListAsync(r => ids.Contains(r.Id))).OrderBy(r => r.Level);
                regionFullName = regions.Select(r => r.Name).ToArray();
            }

            result.RoleId = this.CurrentUser.RoleId;
            result.RoleName = result.Roles?.Find(e => e.RoleId == this.CurrentUser.RoleId)?.RoleName;
            result.RegionId = this.CurrentUser.RegionId;
            result.RegionName = regionInfo?.Name;
            result.RegionFullName = regionFullName;
            result.RegionLevel = regionInfo?.Level ?? RegionLevel.区县;

            return result;
        }

        /// <summary>
        /// 切换当前用户角色
        /// </summary>
        /// <param name="platformType"></param>
        /// <param name="userRoleId">用户角色Id(不是roleId)</param>
        /// <returns></returns>
        [HttpGet("switchrole/{platformType}/{userRoleId}")]
        public async Task<bool> SwitchRoleAsync(PlatformType platformType, long userRoleId)
        {
            var userId = base.CurrentUser.UserId;

            var roles = await userRoleService.GetUserRoles(userId, platformType);

            var role = roles.FirstOrDefault(a => a.Id == userRoleId);

            if (role is null)
            {
                throw new ErrorCodeException(-1, "你要切换的角色已不存在");
            }

            var tokenHash = StringExtensions.EncodeMD5(this.CurrentUser.Token);

            var token = await userTokenService.GetAsync(a => a.TokenHash == tokenHash);

            if (token is null)
            {
                throw new ErrorCodeException(-1, "请先登录");
            }

            token.RoleId = role.RoleId;
            token.RegionId = role.RegionId;

            await userRoleService.ChangeDefaultRoleAsync(userRoleId, userId, platformType);

            //修改userToken 角色信息
            await userTokenService.UpdateAsync(token);

            token.User = await userService.GetAsync(a => a.Id == token.UserId, includes: a => a.Include(c => c.UserRoles).ThenInclude(c => c.Role));

            var cacheData = this.Mapper.Map<UserTokenCacheDto>(token);

            await redisService.SetAsync(CoreRedisConstants.UserToken.Format(token.TokenHash), cacheData, token.ExpirationDate - DateTime.Now);

            return true;
        }

        /// <summary>
        /// 获取当前用户当前角色下有权限的行政区域
        /// </summary>
        /// <param name="parentId">父级 Id</param>
        /// <returns></returns>
        [HttpGet("region")]
        public Task<List<RegionDto>> GetCurrentRegionsAsync(long parentId)
        {
            var spec = Specifications<Region>.Create();
            spec.Query.OrderBy(a => a.Order);
            spec.Query.Where(a => EF.Functions.Like(a.IdSequences, $"%.{this.CurrentUser.RegionId}.%"));

            if (parentId > 0)
            {
                spec.Query.Where(a => a.ParentId == parentId);
            }
            else
            {
                spec.Query.Where(a => a.Id == this.CurrentUser.RegionId);
            }
                
            return regionService.GetListAsync<RegionDto>(spec);
        }
    }
}
