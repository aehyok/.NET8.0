using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Options
{
    public interface IOptions
    {
        /// <summary>
        /// 配置节点名称
        /// </summary>
        string SectionName { get; }
    }
}
