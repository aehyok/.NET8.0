using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MDModel_View2View
	{
		public MDModel_View2View(string id, string targetViewName, string relationString, string title, int order, string param)
		{
			ID = id;
			TargetViewName = targetViewName;
			RelationString = relationString;
			DisplayTitle = title;
			DisplayOrder = order;
			MetaDataParam = param;
		}

		
		public string ID { get; set; }
		
		public string RelationString { get; set; }
		
		public string TargetViewName { get; set; }
		
		public string DisplayTitle { get; set; }
		
		public string ViewGroupID { get; set; }
		
		public int DisplayOrder { get; set; }
		
		public string MetaDataParam { get; set; }
		
		public string QueryModelID { get; set; }
		public override string ToString()
		{
			return this.DisplayTitle;
		}
	}
}
