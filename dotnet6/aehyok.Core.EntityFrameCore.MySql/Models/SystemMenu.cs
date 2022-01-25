using aehyok.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    public class SystemMenu: EntityBase
    {
        public string? MenuName {  get; set; }

        public string? MenuPath { get; set;}

        public string? FatherId { get; set; }

        public int? DisplayOrder { get; set; }

        public string? menuParameter { get; set; }  

        public int? Status { get; set; }
    }
}
