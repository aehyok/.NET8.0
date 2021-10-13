using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MD_View2App
	{
		
		public string ID { get; set; }
		
		public string ViewID { get; set; }
		
		public string Title { get; set; }
		
		public string RegURL { get; set; }
		
		public string AppName { get; set; }
		
		public string Meta { get; set; }
		
		public int DisplayOrder { get; set; }
		
		public int DisplayHeight { get; set; }

		public override string ToString()
		{
			if (Title != null)
			{
				return Title;
			}
			else
			{
				return ID;
			}
		}
	}
}
