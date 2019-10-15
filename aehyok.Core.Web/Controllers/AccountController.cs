using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using aehyok.Core.Model;
using aehyok.Core.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Core.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
        {
            _accountRepository = accountRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel user, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_accountRepository.CheckLogin(user.UserName, user.Password))
                    {
                        
                        var tempUser = new { UserName = user.UserName, Password = user.Password };
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Sid, user.Password)
                        };
                        var indentity = new ClaimsIdentity(claims, "Cookies");
                        var principal = new ClaimsPrincipal(indentity);

                        var authProperties = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                            //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10), 
                            // The time at which the authentication ticket expires. A 
                            // value set here overrides the ExpireTimeSpan option of 
                            // CookieAuthenticationOptions set with AddCookie.
                            IsPersistent = false,
                            //IssuedUtc = <DateTimeOffset>,// The time at which the authentication ticket was issued.
                            RedirectUri = returnUrl ?? "/Home/Index"
                        };
                        await _httpContextAccessor.HttpContext.SignInAsync(principal, authProperties);
                        //if (HttpContext.User.Identity.IsAuthenticated)
                        //{
                        //    return RedirectToAction("Index", "Home", new { id = DateTime.Now.Ticks });
                        //}

                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "用户名或者密码错误,请检查。");
                    }
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return View();
        }
    }
}