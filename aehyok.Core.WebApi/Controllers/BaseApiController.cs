using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[VisualizationResult]
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// 日志
        /// </summary>
        protected Logger _logger;

        /// <summary>
        /// Controller
        /// </summary>
        public BaseApiController()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }
    }
}
