using aehyok.Core.Data.Entity;
using aehyok.Core.Data.Entity.GuideLine;
using aehyok.Core.DataBase;
using aehyok.Core.IRepository;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace aehyok.Core.Repository
{
    public class GuideLineRepository : AbstractBaseRepository, IGuideLineRepository
    {
        public GuideLineRepository(IDbAccossor dbAccossor, ILogger<GuideLineRepository> logger) : base(dbAccossor,logger)
        {
        }


        public MD_GuideLine GetGuidelineDefine(string guidelineId)
        {
            throw new NotImplementedException();
        }

        private const string _sqlGetGuideLineList = @"select z.ID,z.ZBMC,z.XSXH from TJ_ZDYZBDYB z";
        public List<MD_GuideLine> GetGuideLineList()
        {
            List<MD_GuideLine> guideLineList = new List<MD_GuideLine>();
            try
            {
                _dbAccossor.DbDefaultConnection.Using(dbConn =>
                {
                    using (IDataReader dr = dbConn.ExecuteReader(_sqlGetGuideLineList, null, null, null, CommandType.Text))
                    {
                        while (dr.Read())
                        {
                            MD_GuideLine item = new MD_GuideLine();
                            item.Id = dr.IsDBNull(0) ? "" : dr.GetString(0);
                            item.GuideLineName = dr.IsDBNull(1) ? "" : dr.GetString(1);
                            item.DisplayOrder = dr.IsDBNull(2) ? 0 : dr.GetInt32(2);
                            guideLineList.Add(item);
                        }
                    }
                });
            }
            catch (Exception e)
            {
                _logger.LogError("获取指标列表时发生错误，错误信息为" + e.Message);
                throw e;
            }
            return guideLineList;
        }

        public int GetQueryResultCount(string guideLineId, Dictionary<string, object> param, string filterWord)
        {
            throw new NotImplementedException();
        }

        public DataTable QueryGuideline(string guideLineId, Dictionary<string, object> param, string filterWord)
        {
            throw new NotImplementedException();
        }

        public DataTable QueryGuideline(string guideLineId, Dictionary<string, object> param, string filterWord, decimal pageIndex, decimal pageSize, string sortBy, string sortDirection, ref int recordCount)
        {
            throw new NotImplementedException();
        }
    }
}
