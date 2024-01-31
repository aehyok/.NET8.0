using aehyok.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Domains
{
    /// <summary>
    /// 异步任务
    /// </summary>
    public class AsyncTask : AuditedEntity
    {
        /// <summary>
        /// 任务状态
        /// </summary>
        public AsyncTaskState State { get; set; }

        /// <summary>
        /// 任务标识，根据该值判断处理方式
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 任务数据
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 任务返回数据
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryCount { get; set; }
    }

    /// <summary>
    /// 异步任务处理状态
    /// </summary>
    public enum AsyncTaskState
    {
        待处理 = 0,
        处理中 = 1,
        处理完成 = 2,
        失败 = 9
    }
}
