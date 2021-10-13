using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MDModel_View2ViewGroup
	{
		public MDModel_View2ViewGroup(string id, string title, int order)
		{
			ID = id;
			DisplayTitle = title;
			DisplayOrder = order;
		}
		public override string ToString()
		{
			return this.DisplayTitle;
		}

		
		public string ID { get; set; }
		
		public int DisplayOrder { get; set; }
		
		public string DisplayTitle { get; set; }
		
		public List<MDModel_View2View> View2Views { get; set; }
		
		public string QueryModelID { get; set; }

	}
}
