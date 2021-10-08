using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.Data
{
    public class ResultModel
    {
        /// <summary>
        /// 返回的CODE
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
