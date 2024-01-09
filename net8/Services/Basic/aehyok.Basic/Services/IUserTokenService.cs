using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Services
{
    /// <summary>
    /// 用户token服务
    /// </summary>
    public interface IUserTokenService: IServiceBase<UserToken>
    {
        /// <summary>
        /// 生成图片验证码
        /// </summary>
        /// <returns></returns>
        Task<CaptchaDto> GenerateCaptchaAsync();

        /// <summary>
        /// 验证验证码是否正确
        /// </summary>
        /// <param name="captchaCode"></param>
        /// <param name="captchaKey"></param>
        /// <returns></returns>
        Task<bool> ValidateCaptchaAsync(string captchaCode, string captchaKey);

        /// <summary>
        /// 通过账号密码登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        Task<UserTokenDto> LoginWithPasswordAsync(string username, string password, PlatformType platformType);
    }
}
