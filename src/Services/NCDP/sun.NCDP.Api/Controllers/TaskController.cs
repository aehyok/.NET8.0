using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sun.Infrastructure.Models;
using sun.NCDP.Dto;
using X.PagedList;

namespace sun.NCDP.Api.Controllers
{

    /// <summary>
    /// 自动化任务管理
    /// </summary>
    public class TaskController : NCDPControllerBase
    {
        /// <summary>
        /// 自动化任务列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IPagedList<AutoTaskDto>> GetTaskListAsync([FromQuery] PagedQueryModelBase model)
        {
            return null;
        }

        /// <summary>
        /// 获取自动化任务详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<AutoTaskDto> GetTaskDetailAsync(long id)
        {
            return null;
        }

        /// <summary>
        /// 新增自动化任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> PostTaskAsync(CreateAutoTaskDto model)
        {
            return 0;
        }

        /// <summary>
        /// 修改自动化任务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<StatusCodeResult> PutTaskAsync(long id, CreateAutoTaskDto model)
        {
            return Ok();
        }

        /// <summary>
        /// 删除自动化任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<StatusCodeResult> DeleteTaskAsync(long id)
        {
            // 先判断任务是否存在以及任务的状态 
            return Ok();
        }

        /// <summary>
        /// 测试自动化任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("test/{id}")] 
        public async Task<StatusCodeResult> TestAsync(long id)
        {
            return Ok();
        }


        /// <summary>
        /// 发布自动化任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("publish/{id}")]
        public async Task<StatusCodeResult> PublishAsync(long id)
        {
            return Ok();
        }


    }
}
