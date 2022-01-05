using aehyok.Lib.MetaData.Define;
using aehyok.Lib.MetaData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Base.Models
{
    /// <summary>
    /// 保存指标参数定义模型
    /// </summary>
    public class SaveGuideLineParamModel
    {
        /// <summary>
        /// 
        /// </summary>
        public MD_GuideLine_ParamSetting ParamSetting { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public List<MDQuery_GuideLineParameter> Paramters { get; set; }

    }
}
