using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure.Models
{
    /// <summary>
    /// 请求结果返回实体
    /// </summary>
    [Serializable]
    public class RequestResultModel
    {
        public RequestResultModel()
        {
            
        }

        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public Object Data { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }

    /// <summary>
    /// 请求结果分页返回实体
    /// </summary>

    public class  RequestPagedResultModel: RequestResultModel
    {
        /// <summary>
        /// 总数量
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int Limit { get; set; }
    }
}
