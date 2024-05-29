using sun.Core;
using Microsoft.AspNetCore.Mvc;

namespace sun.NCDP.Api.Controllers
{
    /// <summary>
    /// NCDP服务控制器基类，继承自公共服务的控制器基类
    /// </summary>
    [Route("api/ncdp/[controller]")]
    public class NCDPControllerBase : ApiControllerBase
    {

    }
}

