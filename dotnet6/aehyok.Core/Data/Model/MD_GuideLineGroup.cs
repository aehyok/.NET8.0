using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MD_GuideLineGroup
	{


		public MD_GuideLineGroup()
		{
		}


		public MD_GuideLineGroup(string ztmc, string ztsm, string nsName, string ssdw, int lx, int qxlx)
		{
			this.SSDW = ssdw;
			this.ZBZTMC = ztmc;
			this.ZBZTSM = ztsm;
			this.NamespaceName = nsName;
			this.LX = lx;
			this.QXLX = qxlx;
		}

		
		public List<MD_GuideLine> ChildGuideLines { get; set; }
		
		public MD_Nodes MD_Nodes { get; set; }
		
		public string SSDW { get; set; }
		
		public string ZBZTMC { get; set; }
		
		public string ZBZTSM { get; set; }
		
		public int LX { get; set; }
		
		public int QXLX { get; set; }
		
		public string NamespaceName { get; set; }

		public override string ToString()
		{
			return this.ZBZTMC;
		}

	}
}
