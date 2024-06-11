using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sun.Core.Dtos.GuideLine;
using sun.Infrastructure.Models;
using X.PagedList;

namespace sun.NCDP.Api.Controllers
{
    /// <summary>
    /// 自定义指标定义
    /// </summary>
    public class GuideLineController : NCDPControllerBase
    {
        /// <summary>
        /// 获取指标定义列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("guidelinedefine")]
        public async Task<IPagedList<AutoGuideLineDefineDto>> GetGuideLineDefineListAsync([FromQuery] PagedQueryModelBase model)
        {
            return null;
        }

        /// <summary>
        /// 新增指标定义
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("guidelinedefine")]
        public async Task<long> PostGuideLineDefineAsync(CreateAutoGuideLineDefineDto model)
        {
            return 0;
        }

        /// <summary>
        /// 修改指标定义
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("guidelinedefine/{id}")]
        public async Task<StatusCodeResult> PutGuideLineDefineAsync(long id, CreateAutoGuideLineDefineDto model)
        {
            return Ok();
        }

        /// <summary>
        /// 删除指标定义
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("guidelinedefine/{id}")]
        public async Task<StatusCodeResult> DeleteGuideLineDefineAsync(long id)
        {
            return Ok();
        }

        /// <summary>
        /// 启用指标定义
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("guidelinedefine/enable/{id}")]
        public async Task<StatusCodeResult> EnableGuideLineDefineAsync(long id)
        {
            return Ok();
        }

        /// <summary>
        /// 禁用指标定义
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("guidelinedefine/disable/{id}")]
        public async Task<StatusCodeResult> DisableGuideLineDefineAsync(long id)
        {
            return Ok();
        }
    }
}
