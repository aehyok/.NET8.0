using aehyok.Core.EntityFrameCore.MySql.Data;
using aehyok.Core.EntityFrameCore.MySql.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public List<BasicUser> GetUserList()
        {
            var context = new MyDbConext();
            var list = context.BasicUsers.ToList();
            return list;
        }
    }
}
