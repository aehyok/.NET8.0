using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace sun.NCDP.Api.Controllers
{
    /// <summary>
    /// 流程业务数据
    /// </summary>
    public class BusinessController : NCDPControllerBase
    {
        /// <summary>
        /// 获取流转列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("circulate")]
        public async Task<IPagedList<dynamic>> GetCirculateListAsync()
        {
            return null;
        }

        /// <summary>
        /// 流程开始登记数据
        /// </summary>
        /// <returns></returns>
        [HttpPost("start")]
        public async Task<dynamic> StartAsync()
        {
            return null;
        }

        /// <summary>
        /// 提交业务数据
        /// </summary>
        /// <returns></returns>
        [HttpPost("submit")]
        public async Task<dynamic> SubmitAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取当前业务当前状态下可操作的动作列表
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        [HttpGet("getactionlist")]
        public async Task<dynamic> GetActionListAsync(long stateId)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取当前动作的初始化数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("initforaction")]
        public async Task<dynamic> InitForActionAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 提交流转
        /// </summary>
        /// <returns></returns>
        [HttpPost("submitforcirculate")]
        public async Task<dynamic> SubmitForCirculateAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 提交审批
        /// </summary>
        /// <returns></returns>
        [HttpPost("submitforapproval")]
        public async Task<dynamic> SubmitForApprovalAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 流程结束
        /// </summary>
        /// <returns></returns>
        [HttpPost("end")]
        public async Task<dynamic> EndAsync()
        {
            return null;
        }

        /// <summary>
        /// 获取审批列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("approval")]
        public async Task<IPagedList<dynamic>> GetApprovalListAsync()
        {
            return null;
        }
    }
}
