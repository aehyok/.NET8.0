using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.Data.Model
{
    /// <summary>
    /// 指标展示实体
    /// </summary>
    public class GuideLineModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Field { get; set; }

        public bool Checked {get;set;}

        public bool Spread { get; set; }

        public List<GuideLineModel> Children { get; set; }

        public GuideLineModel()
        {
            Children = new List<GuideLineModel>();
        }
    }
}
