using aehyok.Core.Domains;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure.Options;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace aehyok.Core.Services
{
    /// <summary>
    /// 异步任务接口实现
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="mapper"></param>
    public class AsyncTaskService(DbContext dbContext, IMapper mapper) : ServiceBase<AsyncTask>(dbContext, mapper), IAsyncTaskService
    {
        public async Task<AsyncTask> GenerateTaskAsync<TData>(TData data, string code) where TData : new()
        {
            var task = new AsyncTask
            {
                Code = code,
                State = AsyncTaskState.待处理,
                Data = JsonSerializer.Serialize(data, JsonOptions.Default),
                ErrorMessage = string.Empty,
                Result = string.Empty,
                Remark = string.Empty,
            };

            await this.InsertAsync(task);

            // 发布任务
            //await this.eventBus.PublishAsync(new AsyncTaskEvent(task));

            return task;
        }
    }
}
