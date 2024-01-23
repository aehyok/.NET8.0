using aehyok.Core.Domains;
using aehyok.Core.Dtos;
using aehyok.EntityFrameworkCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Services
{
    public interface IRegionService: IServiceBase<Region>
    {
        Task<Core.Domains.File> ExportAsync(RegionExportQueryDto model);
    }
}
