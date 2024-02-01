using sun.Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace sun.Core.Authentication
{
    /// <summary>
    /// 请求认证处理器（Token校验）
    /// </summary>
    public class RequestAuthenticationHandler(IOptionsMonitor<RequestAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUserTokenService userTokenService) : AuthenticationHandler<RequestAuthenticationSchemeOptions>(options, logger, encoder, clock)
    {
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var token = Request.Headers.Authorization.ToString();

            if(!string.IsNullOrEmpty(token))
            {
                token = token.Trim();

                // 验证 Token 是否有效，并获取用户信息
                var userToken = await userTokenService.ValidateTokenAsync(token);
                if (userToken == null)
                {
                    return AuthenticateResult.Fail("Invalid Token!");
                }

                var claims = new List<Claim>
                {
                    new(DvsClaimTypes.RegionId, userToken.RegionId.ToString()),
                    new(DvsClaimTypes.UserId, userToken.UserId.ToString()),
                    new(DvsClaimTypes.Token, token),
                    new(DvsClaimTypes.RoleId, userToken.RoleId.ToString()),
                    new(DvsClaimTypes.PopulationId, userToken.PopulationId.ToString()),
                    new(ClaimTypes.NameIdentifier, userToken.UserId.ToString()),
                    new(DvsClaimTypes.TokenId, userToken.Id.ToString()),
                    new(DvsClaimTypes.PlatFormType, userToken.PlatformType.ToString()),
                };

                // 将当前用户的所有角色添加到 Claims 中
                userToken.Roles.ForEach(a =>
                {
                    claims.Add(new Claim(ClaimTypes.Role, a));
                });

                var claimsIdentity = new ClaimsIdentity(claims, nameof(RequestAuthenticationHandler));

                var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), this.Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            return AuthenticateResult.NoResult();
        }
    }
}
