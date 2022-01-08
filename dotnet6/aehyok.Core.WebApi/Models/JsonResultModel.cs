using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Base.Models
{
    /// <summary>
    /// 返回的JSON结构
    /// </summary>
    public class JsonResultModel
    {
        /// <summary>
        /// 返回的CODE 200为成功
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// 返回的描述
        /// </summary>
        public object Data { get; set; }
    }
}
