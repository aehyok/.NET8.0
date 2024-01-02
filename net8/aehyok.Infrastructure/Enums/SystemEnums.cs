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

        ///// <summary>
        ///// App
        ///// </summary>
        //App = 3,

        ///// <summary>
        ///// 本地客户端
        ///// </summary>
        //Client = 4,
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

    public enum UserType
    {
        全部 = 0,
        //公众用户 = 1,
        工作人员 = 2,
        //网格员 = 3,
        //网格长 = 4,
        //企业管理员 = 5,
        //设施管理员 = 6,
        //游客 = 7
    }
}
