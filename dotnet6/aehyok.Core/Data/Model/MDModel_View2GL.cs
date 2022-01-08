using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MDModel_View2GL
	{
		public MDModel_View2GL(string id, string glid, string viewid, int order, string title, string param)
		{
			ID = id;
			DisplayOrder = order;
			DisplayTitle = title;
			ViewID = viewid;
			TargetGuideLineID = glid;
			RelationParam = param;
		}
		
		public string ID { get; set; }
		
		public int DisplayOrder { get; set; }
		
		public string DisplayTitle { get; set; }
		
		public string ViewID { get; set; }
		
		public string TargetGuideLineID { get; set; }
		
		public string RelationParam { get; set; }
	}
}
