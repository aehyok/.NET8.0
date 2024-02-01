using sun.Core.Domains;
using sun.RabbitMQ.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.EventData
{
    /// <summary>
    /// 异步任务传输数据
    /// </summary>
    public class AsyncTaskEventData: EventBase
    {
        public AsyncTaskEventData()
        { }

        public AsyncTaskEventData(long id, string code)
        {
            this.TaskId = id;
            this.TaskCode = code;
        }
        public AsyncTaskEventData(AsyncTask asyncTask)
            : this(asyncTask.Id, asyncTask.Code)
        {

        }


        
        /// <summary>
        /// 异步任务编码
        /// </summary>
        public string TaskCode { get; set; }

        /// <summary>
        /// 异步任务Id
        /// </summary>
        public long TaskId { get; set; }
    }
}
