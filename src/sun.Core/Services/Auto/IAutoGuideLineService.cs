using sun.Core.Domains;
using sun.Core.Domains.Auto;
using sun.EntityFrameworkCore.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Services
{
    public interface IAutoGuideLineService: IServiceBase<AutoGuideLineDefine>
    {
        Task<DataTable> QueryCustomFormGuideline(string guideLineId, Dictionary<string, object> sqlParameters, string keyword, long areaid);
    }
}
