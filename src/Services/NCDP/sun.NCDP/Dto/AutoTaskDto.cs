using sun.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.NCDP.Dto
{
    public class AutoTaskQueryModel : PagedQueryModelBase
    {

        public long RegionId { get; set; }
    }

    public class AutoTaskDto
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public long Id { get; set; }

        public string AutoTaskName { get; set; }
    }

    public class  CreateAutoTaskDto 
    {
        
    }
}
