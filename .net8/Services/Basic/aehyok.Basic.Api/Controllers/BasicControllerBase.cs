using aehyok.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Basic.Api.Controllers
{
    /// <summary>
    /// 基础服务控制器基类，继承自公共服务的控制器基类
    /// </summary>
    [Route("api/basic/[controller]")]
    public class BasicControllerBase : ApiControllerBase
    {

    }
}
