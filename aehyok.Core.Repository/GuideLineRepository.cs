using aehyok.Core.Data.Entity;
using aehyok.Core.Data.Entity.GuideLine;
using aehyok.Core.DataBase;
using aehyok.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace aehyok.Core.Repository
{
    public class GuideLineRepository : AbstractBaseRepository, IGuideLineRepository
    {
        public GuideLineRepository(IDbAccossor dbAccossor) : base(dbAccossor)
        {
        }

        public IDbAccossor DbAccossor => base.dbAccossor;

        public MD_GuideLine GetGuidelineDefine(string guidelineId)
        {
            throw new NotImplementedException();
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
