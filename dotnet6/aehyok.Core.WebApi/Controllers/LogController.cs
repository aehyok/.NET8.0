using aehyok.Core.EntityFramework.MySql;
using aehyok.Core.EntityFramework.MySql.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace aehyok.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : BaseApiController
    {
        private readonly IRepository<CiCdLog> _logRepository;

        public LogController(IRepository<CiCdLog> logRepository)
        {
            _logRepository = logRepository; 
        }


        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]

        public List<CiCdLog> GetLogList()
        {
            var list = _logRepository.GetList();
            return list;
        }
    }
}
