using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        public IMapper Mapper
        {
            get
            {
                return HttpContext.RequestServices.GetService<IMapper>();
            }
        }

        /// <summary>
        /// 当前用户
        /// </summary>
        public ICurrentUser CurrentUser
        {
            get
            {
                return HttpContext.RequestServices.GetService<ICurrentUser>();
            }
        }
    }
}
