using aehyok.Core.EntityFramework.MySql;
using aehyok.Core.EntityFramework.MySql.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

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

        public async Task<IPagedList<CiCdLog>> GetLogList(int page, int limit)
        {
            //var list = _logRepository.GetQueryable().OrderByDescending(item => item.CreateTime).ToList();
            Expression<Func<CiCdLog, bool>> filter = item => !string.IsNullOrEmpty(item.Id);
            var list = await _logRepository.GetPagedListAsync(filter, page, limit);

            return list;
        }
    }
}
