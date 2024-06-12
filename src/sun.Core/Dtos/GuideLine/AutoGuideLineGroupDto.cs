using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Dtos.GuideLine
{
    /// <summary>
    /// 指标组元数据定义
    /// </summary>
    public class AutoGuideLineGroupDto
    {

        public AutoGuideLineGroupDto()
        {
        }
        public AutoGuideLineGroupDto(string _ztmc, string _ztsm, string _nsName, string ssdw, int lx, int qxlx)
        {
            this.SSDW = ssdw;
            this.ZBZTMC = _ztmc;
            this.ZBZTSM = _ztsm;
            this.NamespaceName = _nsName;
            this.LX = lx;
            this.QXLX = qxlx;
        }

        [DataMember]
        public List<GuideLineDefineDto> ChildGuideLines { get; set; }
        ///[DataMember]
        /// public MD_Nodes MD_Nodes { get; set; }
        [DataMember]
        public string SSDW { get; set; }
        [DataMember]
        public string ZBZTMC { get; set; }
        [DataMember]
        public string ZBZTSM { get; set; }
        [DataMember]
        public int LX { get; set; }
        [DataMember]
        public int QXLX { get; set; }
        [DataMember]
        public string NamespaceName { get; set; }

        public override string ToString()
        {
            return this.ZBZTMC;
        }
    }
}
