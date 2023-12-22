using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure.Enums
{
    /// <summary>
    /// 菜单属于哪个平台
    /// </summary>
    public enum PlatformType
    {
        /// <summary>
        /// PC浏览器
        /// </summary>
        Pc = 1,

        /// <summary>
        /// 微信小程序
        /// </summary>
        Mini = 2,

        /// <summary>
        /// App
        /// </summary>
        App = 3,

        /// <summary>
        /// 本地客户端
        /// </summary>
        Client = 4,
    }

    /// <summary>
    /// 登陆方式
    /// </summary>
    public enum LoginMethodType
    {
        账号密码 = 0,
        短信验证码 = 1,
        小程序登录 = 2,
        扫码登录 = 3
    }
}
