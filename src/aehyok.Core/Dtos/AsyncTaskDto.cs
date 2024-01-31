using aehyok.Core.Domains;
using aehyok.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Dtos
{
    public class AsyncTaskDto : DtoBase
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
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 任务返回数据
        /// </summary>
        public string Result { get; set; }
    }
}
