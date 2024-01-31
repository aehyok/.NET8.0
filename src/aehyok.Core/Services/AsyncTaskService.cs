using aehyok.Core.Domains;
using aehyok.Core.Dtos;
using aehyok.Core.EventData;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure;
using aehyok.Infrastructure.Options;
using aehyok.RabbitMQ.EventBus;
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
    public class AsyncTaskService(DbContext dbContext, IMapper mapper, IEventPublisher publisher) : ServiceBase<AsyncTask>(dbContext, mapper), IAsyncTaskService,IScopedDependency
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
            publisher.Publish(new AsyncTaskEventData(task));

            return task;
        }
    }
}
