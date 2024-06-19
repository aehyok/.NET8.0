using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sun.Core.Dtos.Auto;
using sun.Infrastructure.Models;
using X.PagedList;

namespace sun.NCDP.Api.Controllers
{
    /// <summary>
    /// 自定义表单
    /// </summary>
    public class FormController : NCDPControllerBase
    {
        /// <summary>
        /// 获取form表单配置列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("define")]
        public async Task<IPagedList<AutoFormDefineDto>> GetFormDefineListAsync([FromQuery] PagedQueryModelBase model)
        {
            return null;
        }

        /// <summary>
        /// 新增form表单定义
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("define")]
        public async Task<long> PostFormDefineAsync(AutoFormDefineDto model)
        {
            return 0;
        }

        /// <summary>
        /// 修改form表单定义
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("define/{id}")]
        public async Task<StatusCodeResult> PutFormDefineAsync(long id, AutoFormDefineDto model)
        {
            return Ok();
        }

        /// <summary>
        /// 删除form表单定义
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("define/{id}")]
        public async Task<StatusCodeResult> DeleteFormDefineAsync(long id)
        {
            return Ok();
        }

        /// <summary>
        /// 启用form表单定义
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("define/enable/{id}")]
        public async Task<StatusCodeResult> EnableFormDefineAsync(long id)
        {
            return Ok();
        }

        /// <summary>
        /// 禁用form表单定义
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("define/disable/{id}")]
        public async Task<StatusCodeResult> DisableFormDefineAsync(long id)
        {
            return Ok();
        }


        /// <summary>
        /// form表单的初始化
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("init/{id}")]
        public async Task<AutoFormDto> InitAsync(long id)
        {
            return null;
        }

        /// <summary>
        /// form表单的提交
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("submit/{id}")]
        public async Task<StatusCodeResult> SubmitAsync(long id, AutoFormSubmitDto model)
        {
            return Ok();
        }

        /// <summary>
        /// form表单的查看数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("view/{id}")]
        public async Task<AutoFormDto> ViewAsync(long id)
        {
            return null;
        }

        /// <summary>
        /// form表单的预览
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("preview/{id}")]
        public async Task<AutoFormDto> PreviewAsync(long id)
        {
            return null;
        }

        /// <summary>
        /// form表单提交记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("submitList/{id}")]
        public async Task<IPagedList<dynamic>> SubmitListAsync(long id, [FromQuery] PagedQueryModelBase model)
        {
            return null;
        }
    }
}
