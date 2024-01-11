using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Attributes
{
    /// <summary>
    /// 操作日志特性
    /// </summary>
    public class OperationLogActionAttribute: Attribute
    {
        /// <summary>
        /// 操作日志内容模板
        /// </summary>
        public string MessageTemplate{ get; set; }

        public OperationLogActionAttribute(string messageTemplate)
        {
            MessageTemplate = messageTemplate;
        }
    }
}
