using aehyok.Basic.Dtos;
using aehyok.Basic.Services;
using aehyok.Core;
using aehyok.Core.Dtos;
using aehyok.Core.Services;
using aehyok.Infrastructure;
using aehyok.Infrastructure.Enums;
using aehyok.Infrastructure.Exceptions;
using aehyok.Redis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StringExtensions = aehyok.Infrastructure.StringExtensions;

namespace aehyok.Basic.Api.Controllers
{

    /// <summary>
    /// Token 管理
    /// </summary>
    public class TokenController(IUserTokenService userTokenService, IUserRoleService userRoleService,IUserService userService, IRedisService redisService) : BasicControllerBase
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
        /// 切换当前用户角色
        /// </summary>
        /// <param name="platformType"></param>
        /// <param name="userRoleId">用户角色Id(不是roleId)</param>
        /// <returns></returns>
        [HttpGet("switchrole/{platformType}")]
        public async Task<bool> SwitchRoleAsync(PlatformType platformType,long userRoleId)
        {
            var userId = base.CurrentUser.UserId;

            var roles = await userRoleService.GetUserRoles(userId, platformType);

            var role = roles.FirstOrDefault(a => a.Id == userRoleId);

            if(role is null)
            {
                throw new ErrorCodeException(-1, "你要切换的角色已不存在");
            }

            var tokenHash = StringExtensions.EncodeMD5(this.CurrentUser.Token);

            var token = await userTokenService.GetAsync(a => a.TokenHash == tokenHash);

            if(token is null)
            {
                throw new ErrorCodeException(-1, "请先登录");
            }

            token.RoleId = role.RoleId;
            token.RegionId = role.RegionId;

            //修改userToken 角色信息
            await userTokenService.UpdateAsync(token);

            token.User = await userService.GetAsync(a => a.Id == token.UserId, includes: a => a.Include(c => c.UserRoles).ThenInclude(c => c.Role));

            var cacheData = this.Mapper.Map<UserTokenCacheDto>(token);

            await redisService.SetAsync(CoreRedisConstants.USER_TOKEN_CACHE_KEY_PATTERN.Format(token.TokenHash), cacheData, token.ExpirationDate - DateTime.Now);

            return true;
        }
    }
}
