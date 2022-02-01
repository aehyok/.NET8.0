using System;
using aehyok.Base;

namespace aehyok.Core.EntityFramework.MySql.Models
{
	public class SystemDictionaryType: EntityBase
	{
        public string? Code { get; set; }

        public string? Name { get; set; }

        public int? IsDelete { get; set; }

        public string? Remark { get; set; }

    }
}

