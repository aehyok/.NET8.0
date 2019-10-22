using aehyok.Core.Data;
using aehyok.Core.Data.Entity.GuideLine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace aehyok.Core.IRepository
{
    /// <summary>
    /// 指标定义接口
    /// </summary>
    public interface IGuideLineRepository : IRepository
    {
        /// <summary>
        /// 取指标定义
        /// </summary>
        /// <param name="guidelineId"></param>
        /// <returns></returns>
        MD_GuideLine GetGuidelineDefine(string guidelineId);

        /// <summary>
        /// 取指标结果记录数
        /// </summary>
        /// <param name="guideLineId"></param>
        /// <param name="param"></param>
        /// <param name="filterWord"></param>
        /// <returns></returns>
        int GetQueryResultCount(string guideLineId, Dictionary<string, object> param, string filterWord);

        /// <summary>
        /// 取指标结果集
        /// </summary>
        /// <param name="guideLineId"></param>
        /// <param name="param"></param>
        /// <param name="filterWord"></param>
        /// <returns></returns>
        DataTable QueryGuideline(string guideLineId, Dictionary<string, object> param, string filterWord);

        /// <summary>
        /// 分页取指标结果集
        /// </summary>
        /// <param name="guideLineId"></param>
        /// <param name="param"></param>
        /// <param name="filterWord"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortDirection"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        DataTable QueryGuideline(string guideLineId, Dictionary<string, object> param, string filterWord, decimal pageIndex, decimal pageSize, string sortBy, string sortDirection, ref int recordCount);

    }
}
