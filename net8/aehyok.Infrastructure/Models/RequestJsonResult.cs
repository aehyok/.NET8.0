using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Infrastructure.Models
{
    /// <summary>
    /// 继承JsonResult，用于统一返回数据格式
    /// </summary>
    public class RequestJsonResult : JsonResult
    {
        public RequestJsonResult(object value) : base(value)
        {
        }
    }
}
