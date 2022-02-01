using System;
using aehyok.Base;

namespace aehyok.Core.EntityFramework.MySql.Models
{
	public class SystemDictionary: EntityBase
	{
		public string? Code { get; set; }

		public string? Name { get; set; }

		public string? FatherCode { get; set; }

		public int? DisplayOrder { get; set; }

		public string? Remark { get; set; }

		public int? IsDelete { get; set; }
	}
}

