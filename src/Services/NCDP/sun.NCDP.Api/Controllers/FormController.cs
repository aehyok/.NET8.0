using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sun.Infrastructure.Models;
using sun.NCDP.Dto;
using X.PagedList;

namespace sun.NCDP.Api.Controllers
{
    /// <summary>
    /// 自定义表单
    /// </summary>
    public class FormController : NCDPControllerBase
    {
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
