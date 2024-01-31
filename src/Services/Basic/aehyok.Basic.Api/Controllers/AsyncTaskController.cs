using aehyok.Core.Dtos;
using aehyok.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Basic.Api.Controllers
{
    /// <summary>
    /// 异步任务
    /// </summary>
    public class AsyncTaskController : BasicControllerBase
    {
        private readonly IAsyncTaskService asyncTaskService;

        public AsyncTaskController(IAsyncTaskService asyncTaskService)
        {
            this.asyncTaskService = asyncTaskService;
        }

        /// <summary>
        /// 获取任务状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<AsyncTaskDto> GetAsync(long id)
        {
            return this.asyncTaskService.GetAsync<AsyncTaskDto>(a => a.Id == id);
        }
    }
}
