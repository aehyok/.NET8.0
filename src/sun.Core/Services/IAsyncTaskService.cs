using sun.Core.Domains;
using sun.EntityFrameworkCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Services
{
    /// <summary>
    /// 异步任务接口定义
    /// </summary>
    public interface IAsyncTaskService: IServiceBase<AsyncTask>
    {
        /// <summary>
        /// 生成任务
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data">任务数据</param>
        /// <param name="code">任务标识</param>
        /// <returns></returns>
        Task<AsyncTask> GenerateTaskAsync<TData>(TData data, string code) where TData : new();
    }
}
