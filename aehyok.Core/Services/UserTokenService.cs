using aehyok.Basic.Domains;
using aehyok.Core.Domains;
using aehyok.Core.Dtos;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure;
using aehyok.Infrastructure.Captcha;
using aehyok.Infrastructure.Enums;
using aehyok.Infrastructure.Exceptions;
using aehyok.Redis;
using aehyok.Serilog;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StringExtensions = aehyok.Infrastructure.StringExtensions;

namespace aehyok.Core.Services
{
    /// <summary>
    /// 用户Token服务
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="mapper"></param>
    /// <param name="redisService"></param>
    /// <param name="userService"></param>
    /// <param name="httpContextAccessor"></param>
    public class UserTokenService(
        DbContext dbContext, 
        IMapper mapper, 
        IRedisService redisService,
        IUserService userService, 
        IUserRoleService userRoleService,
        IHttpContextAccessor httpContextAccessor) : ServiceBase<UserToken>(dbContext, mapper), IUserTokenService, IScopedDependency
    {
        public async Task<CaptchaDto> GenerateCaptchaAsync()
        {
            // 生成验证码字符
            var captchaCode = Randomizer.Next(4, exceptChar: new char[] { 'o', 'O', '0', '1', 'I', 'l' }, hasSpecialChars: false);

            // 生成验证码图片
            var bytes = CaptchaHelper.GenerateCaptchaImage(120, 48, captchaCode);

            var captcha = new CaptchaDto
            {
                Captcha = $"data:image/jpeg;base64,{Convert.ToBase64String(bytes)}",
                Key = Guid.NewGuid().ToString("N"),
                ExpireTime = DateTime.Now.AddMinutes(10)
            };

            // 将验证码存储到缓存中，有效期 10 分钟
            await redisService.SetAsync(CoreRedisConstants.TokenCaptcha.Format(captcha.Key), captchaCode.ToLower(), TimeSpan.FromMinutes(10));

            return captcha;
        }

        public async Task<bool> ValidateCaptchaAsync(string captchaCode, string captchaKey)
        {

            var cachedCaptcha = await redisService.GetAsync<string>(CoreRedisConstants.TokenCaptcha.Format(captchaKey));

            // 删除缓存
            await redisService.DeleteAsync(CoreRedisConstants.TokenCaptcha.Format(captchaKey));

            // 因为验证码存入缓存时转为了小写，所以这里转小写后对比
            return !cachedCaptcha.IsNullOrEmpty() && cachedCaptcha == captchaCode.ToLower();
        }

        /// <summary>
        /// 账号密码登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<UserTokenDto> LoginWithPasswordAsync(string userName, string password, PlatformType platform)
        {
            var user = await userService.GetAsync(a => a.UserName == userName);

            // 如果根据用户名没有查找到数据，并且用户名是一个手机号码，则使用用户名匹配手机号码
            if (user is null && userName.IsMobile())
            {
                user = await userService.GetAsync(a => a.Mobile == userName);
            }

            if (user == null)
            {
                throw new ErrorCodeException(100002, "账号或密码错误");
            }

            if (user.PasswordSalt.IsNullOrEmpty())
            {
                throw new ErrorCodeException(100003, "该用户还未设置密码");
            }

            // swagger特殊登录
            if(password.StartsWith("swagger"))
            {
                password = password[7..];
            }
            else
            {
                //前端传递的密码是经过base64位处理过的
                password = password.Base64ToString();
                //!等特殊字符会被转义，这里需要解码
                password = Uri.UnescapeDataString(password);
            }

            if (!user.Password.Equals(password.EncodePassword(user.PasswordSalt)))
            {
                throw new ErrorCodeException(100002, "账号或密码错误");
            }

            return await GenerateUserTokenAsync(user, platform);
        }

        /// <summary>
        /// 生成用户 Token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        private async Task<UserTokenDto> GenerateUserTokenAsync(User user, PlatformType platform)
        {
            if (!user.IsEnable)
            {
                throw new ErrorCodeException(100001, "该账号已禁用");
            }

            // 更新最后登录时间
            await userService.UpdateFromQueryAsync(a => a.Id == user.Id, a => new User
            {
                LastLoginTime = DateTime.Now
            });

            var ipAddress = httpContextAccessor.HttpContext.Request.GetRemoteIpAddress();
            var userAgent = httpContextAccessor.HttpContext.Request.Headers.UserAgent.ToString();

            var token = new UserToken()
            {
                ExpirationDate = DateTime.Now.AddHours(2),
                IpAddress = ipAddress.ToString(),
                PlatformType = platform,
                UserAgent = userAgent,
                UserId = user.Id,
                LoginType = LoginType.Login,
                RefreshTokenIsAvailable = true
            };

            token.Token = StringExtensions.GenerateToken(user.Id.ToString(), token.ExpirationDate);
            token.TokenHash = StringExtensions.EncodeMD5(token.Token);
            token.RefreshToken = StringExtensions.GenerateToken(token.Token, token.ExpirationDate.AddMonths(1));

            // 获取用户默认角色信息
            var role = await userRoleService.GetUserDefaultRole(user.Id);
            if (role != null)
            {
                token.RoleId = role.RoleId;
                token.RegionId = role.RegionId;
            }
            //else
            //{
            //    // 如果用户没用任何角色，则给该用户添加游客角色
            //    var guestRole = await this.userRoleService.AddGuestRoleAsync(user.Id);
            //    token.RoleId = guestRole.RoleId;
            //    token.RegionId = guestRole.RegionId;
            //}

            await this.InsertAsync(token);

            // 从数据库中加载 User 对象，以存储到缓存中
            token.User = await userService.GetAsync(a => a.Id == user.Id, includes: a => a.Include(c => c.UserRoles).ThenInclude(c => c.Role));

            var cacheData = this.Mapper.Map<UserTokenCacheDto>(token);

            // 将 Token 信息存储到 Redis，有效期 2 小时
            await redisService.SetAsync(CoreRedisConstants.UserToken.Format(token.TokenHash), cacheData, TimeSpan.FromHours(2));
            return this.Mapper.Map<UserTokenDto>(token);
        }

        /// <summary>
        /// 验证用户 Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<UserTokenCacheDto> ValidateTokenAsync(string token)
        {
            var tokenHash = StringExtensions.EncodeMD5(token);
            var cacheValue = await redisService.GetAsync<UserTokenCacheDto>(CoreRedisConstants.UserToken.Format(tokenHash));

            // 如果 Redis 中没有数据，是否需要查询一次数据库？
            return cacheValue;
        }

        /// <summary>
        /// 使用 Refresh Token 获取新 Token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public virtual async Task<UserTokenDto> RefreshTokenAsync(long userId, string refreshToken)
        {
            if (refreshToken.IsNullOrEmpty())
            {
                throw new ErrorCodeException(-1, "Refresh Token 无效");
            }

            var userToken = await this.GetAsync(a => a.RefreshToken == refreshToken);
            if (userToken is null || !userToken.RefreshTokenIsAvailable || userToken.UserId != userId)
            {
                throw new ErrorCodeException(-1, "Refresh Token 无效");
            }

            // RefreshToken 有效期为一个月
            if (userToken.CreatedAt < DateTime.Now.AddMonths(-1))
            {
                throw new ErrorCodeException(-1, "Refresh Token 已过期");
            }

            var user = await userService.GetAsync(a => a.Id == userToken.UserId);

            // 生成 Token
            var token = await GenerateUserTokenAsync(user, userToken.PlatformType);

            user.LastLoginTime = DateTime.Now;
            await userService.UpdateAsync(user);

            userToken.RefreshTokenIsAvailable = false;
            await this.UpdateAsync(userToken);

            return token;
        }
    }
}
