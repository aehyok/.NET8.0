//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using aehyok.Base.Models;
//using aehyok.Lib;
//using aehyok.Lib.MetaData.Define;
//using aehyok.Lib.Services;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Threading.Tasks;
//using X.PagedList;
//using aehyok.Core.MySqlDataAccessor;

//namespace aehyok.Base.Controllers
//{
//    /// <summary>
//    /// 指标查询方法
//    /// </summary>
//    [Route("/api/mdquery")]
//    public class MdGuidelineController //: MDControllerBase
//    {
//        //private readonly IEncryptionService encryptionService;
//        //private readonly IMetaDataQuery mdService;

//        //public MdGuidelineController(IEncryptionService encryptionService,
//        //     IMetaDataQuery mdService)
//        //{
//        //    this.encryptionService = encryptionService;
//        //    this.mdService = mdService;
//        //}


//        /// <summary>
//        /// 获取指标定义
//        /// </summary>
//        /// <param name="guideLineId"></param>
//        /// <returns></returns>
//        [HttpPost("GetGuidelineDefine")]
//        [AllowAnonymous]
//        public async Task<MD_GuideLine> GetGuidelineDefine(string guideLineId)
//        {
//            var t = await GuidelineAccessor.GetGuidelineDefine(guideLineId);
//            return t;
//        }


//        /// <summary>
//        /// 取指标全部数据 1438771804981694464
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        [HttpPost("GetGuidelineData")]
//        [AllowAnonymous]
//        public async Task<DataTable> GetGuidelineData(QueryGuidelineModel model)
//        {
//            var t = await GuidelineAccessor.QueryGuideline(model.GuideLineId, model.Param, model.FilterWord);

//            return t;
//        }

//        /// <summary>
//        /// 分页取指标数据
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        [HttpPost("GetGuidelineDataPaged")]
//        [AllowAnonymous]
//        public async Task<IPagedList<DataTable>> GetGuidelineDataPaged(QueryGuidelinePageModel model)
//        {
//            //Dictionary<string, object> param = new Dictionary<string, object>();
//            //param.Add("p_nf", 2021);
//            //param.Add("p_hy", 4010100);

//            // var t = await GuidelineAccessor.QueryGuideline("1438708299947577344", param, "");

//            var t = await GuidelineAccessor.QueryGuideline(model.GuideLineId, model.Param, model.FilterWord, model.PageIndex, model.PageSize, model.SortBy, model.SortDirection);

//            return t;
//        }



//    }
//}
