using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
using aehyok.EntityFramework.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Services
{
    public interface IUserTokenService: IServiceBase<UserToken>
    {
        /// <summary>
        /// 生成图片验证码
        /// </summary>
        /// <returns></returns>
        Task<CaptchaDto> GenerateCaptchaAsync();
    }
}
