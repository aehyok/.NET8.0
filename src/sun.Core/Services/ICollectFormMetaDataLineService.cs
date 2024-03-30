using sun.Core.Domains;
using sun.EntityFrameworkCore.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Services
{
    public interface ICollectFormMetaDataLineService: IServiceBase<CollectFormMetaDataLine>
    {
        Task<DataTable> QueryCustomFormGuideline(string guideLineId, Dictionary<string, object> sqlParameters, string keyword, long areaid);
    }
}
