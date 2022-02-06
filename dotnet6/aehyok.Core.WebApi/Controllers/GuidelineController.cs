using aehyok.Core.MySqlDataAccessor;
using aehyok.Lib;
using aehyok.Lib.MetaData.Define;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.WebApi.Controllers
{
    /// <summary>
    /// 指标管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GuidelineController : BaseApiController
    {
        private readonly IMetaDataManager mdService;

        public GuidelineController(
             IMetaDataManager mdService)
        {
            this.mdService = mdService;
        }

        /// <summary>
        /// 取父指标下的所有子指标
        /// </summary>
        /// <param name="FatherGuildLineID"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IList<MD_GuideLine>> GetChildGuideLines(string FatherGuildLineID)
        {
            var ret = await this.mdService.GetChildGuideLines(FatherGuildLineID);
            return ret;
        }

        /// <summary>
        /// 获取指标定义
        /// </summary>
        /// <param name="guideLineId"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<MD_GuideLine> GetGuidelineDefine(string guideLineId)
        {
            var t = await GuidelineAccessor.GetGuidelineDefine(guideLineId);
            return t;
        }

        /// <summary>
        /// 插入新的指标
        /// </summary>
        /// <param name="GuideLineName"></param>
        /// <param name="FatherID"></param>
        /// <param name="GuideLineGroupName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> InsertNewGuideLine(string GuideLineName, decimal FatherID, string GuideLineGroupName)
        {
            var ret = await this.mdService.SaveNewGuideLine(GuideLineName, FatherID, GuideLineGroupName);
            return ret;
        }


        /// <summary>
        /// 保存指标定义
        /// </summary>
        /// <param name="GuideLine"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> SaveGuideLine(MD_GuideLine GuideLine)
        {
            var ret = await this.mdService.SaveGuideLine(GuideLine);
            return ret;
        }

        /// <summary>
        /// 删除指定指标
        /// </summary>
        /// <param name="GuideLineID"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DelGuideLine(string GuideLineID)
        {
            var ret = await this.mdService.DelGuideLine(GuideLineID);
            return ret;
        }
    }
}
