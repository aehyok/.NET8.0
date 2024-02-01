using sun.Core.Domains;
using sun.Core.Dtos;
using sun.EntityFrameworkCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Basic.Services
{
    public interface IRegionService: IServiceBase<Region>
    {
        Task<Core.Domains.File> ExportAsync(RegionExportQueryDto model);
    }
}
