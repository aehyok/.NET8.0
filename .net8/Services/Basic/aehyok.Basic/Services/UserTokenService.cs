using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
using aehyok.EntityFramework.Repository;
using aehyok.Infrastructure;
using aehyok.Infrastructure.Captcha;
using aehyok.Redis;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Services
{
    public class UserTokenService : ServiceBase<UserToken>, IUserTokenService, IScopedDependency
    {
        private readonly IRedisService redisService;
        public UserTokenService(DbContext dbContext, IMapper mapper,IRedisService redisService) : base(dbContext, mapper)
        {
            this.redisService = redisService;
        }

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
            await this.redisService.SetAsync(BasicRedisConstants.CAPTCHA_CACHE_KEY_PATTERN.Format(captcha.Key), captchaCode.ToLower(), TimeSpan.FromMinutes(10));

            return captcha;
        }
    }
}
