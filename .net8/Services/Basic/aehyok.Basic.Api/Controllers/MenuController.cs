using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Basic.Api.Controllers
{

    /// <summary>
    /// 菜单管理
    /// </summary>
    public class MenuController : BasicControllerBase
    {
        /// <summary>
        /// 测试接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task Test()
        {
            throw new Exception("测试异常");
        }
    }
}
