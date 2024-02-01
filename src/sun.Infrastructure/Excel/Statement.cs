using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure.Excel
{
    public abstract class Statement
    {
        /// <summary>
        /// 开始位置
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// 结束位置
        /// </summary>
        public int End { get; set; }

        public int Order { get; set; }
    }

    public class IteratorStatement : Statement
    {
        public string ItemName { get; set; }

        public string DataName { get; set; }

        public override string ToString()
        {
            return $"Iterator Statement:[{Start}:{End}]，Order:{Order}";
        }
    }

    public class ConditionalStatement : Statement
    {
        public override string ToString()
        {
            return $"Conditional Statement:[{Start}:{End}]，Order:{Order}";
        }
    }
}
