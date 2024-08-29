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
        /// 业务列表(根据个人)
        /// </summary>
        /// <returns></returns>
        [HttpGet("circulate")]
        public async Task<IPagedList<dynamic>> GetCirculateListAsync()
        {
            return null;
        }

        /// <summary>
        /// 业务列表(根据区域)
        /// </summary>
        /// <returns></returns>
        [HttpGet("assign")]
        public async Task<IPagedList<dynamic>> GetAssignListAsync()
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
        [HttpGet("actionlist")]
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
        /// 业务指派
        /// </summary>
        /// <returns></returns>
        [HttpPost("assign")]

        public async Task<IPagedList<dynamic>> AssignAsync()
        {
            return null;
        }

        /// <summary>
        /// 业务删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]

        public async Task<IPagedList<dynamic>> DeleteAsync(long id)
        {
            return null;
        }

        /// <summary>
        /// 待审批列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("approval")]
        public async Task<IPagedList<dynamic>> PendApprovalListAsync()
        {
            return null;
        }

        /// <summary>
        /// 审批同意
        /// </summary>
        /// <returns></returns>
        [HttpPost("approval/agree")]
        public async Task<dynamic> ApprovalAgreeAsync()
        {
            return null;
        }

        /// <summary>
        /// 审批不同意
        /// </summary>
        /// <returns></returns>
        [HttpPost("approval/disagree")]
        public async Task<dynamic> ApprovalDisAgreeAsync()
        {
            return null;
        }

        /// <summary>
        /// 报上级审批
        /// </summary>
        /// <returns></returns>
        [HttpPost("approval/report")]
        public async Task<dynamic> ApprovalReportAsync()
        {
            return null;
        }
    }
}
