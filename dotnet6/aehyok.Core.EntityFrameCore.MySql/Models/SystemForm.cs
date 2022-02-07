using aehyok.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    public class SystemForm: EntityBase
    {
        public string? FormName { get; set; }

        //[NotMapped]
        public string? ColumnList { get; set; }

        public string? TableList { get; set; }

        public int? DisplayOrder { get; set; }

        public string? Remark { get; set; }
    }
}
