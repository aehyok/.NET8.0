using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sun.Core.Dtos.GuideLine;
using sun.Infrastructure.Models;
using System.Data;
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



        /// <summary>
        /// 获取指标定义
        /// </summary>
        /// <param name="guideLineId"></param>
        /// <returns></returns>
        [HttpGet("guidelinededata/define/{guideLineId}")]
        public async Task<GuideLineDefineDto> GetGuidelineDefineAsync(string guideLineId)
        {
            return null;
        }

        /// <summary>
        /// 获取多个指标的指标定义
        /// </summary>
        /// <param name="guideLineIds"></param>
        /// <returns></returns>
        [HttpGet("guidelinededata/defines")]
        public async Task<List<GuideLineDefineDto>> GetGuidelineDefinesAsync(string[] guideLineIds)
        {
            return null;
        }

        /// <summary>
        /// 取指标全部数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("guidelinededata/list")]
        public async Task<DataTable> GetGuidelineDataAsync(GuideLineQueryDto model)
        {
            return null;
        }

        /// <summary>
        /// 通过指标取单条记录信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("guidelinededata")]
        public async Task<dynamic> GetGuidelineSingleData(GuideLineQueryDto model)
        {
            return null;
        }

        /// <summary>
        /// 分页取指标数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("guidelinededata/pagelist")]
        public async Task<object> GetGuidelineDataPagedAsync(GuideLinePageQueryDto model)
        {
            return null;
        }
    }
}
