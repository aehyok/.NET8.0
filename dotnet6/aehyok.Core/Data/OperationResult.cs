using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data
{
    public class OperationResult
    {
        /// <summary>
        /// 返回消息字符串
        /// </summary>
        
        public string Message { get; set; }

        /// <summary>
        /// 操作是否成功（true为成功，false为失败）
        /// </summary>
        
        public bool Success { get; set; }

        /// <summary>
        /// 返回值
        /// </summary>
        
        public object ReturnValue { get; set; }

        /// <summary>
        /// 返回字典值
        /// </summary>
        /// 非基础类型(如：实体类、字典、list等)WCF无法从object中序列化，object不属于指定类型
        
        public Dictionary<string, string> ReturnDictionary { get; set; }
    }

    /// <summary>
    /// 操作结果信息返回
    /// </summary>
    public class OperationResult<T> : OperationResult
    {
        /// <summary>
        /// 返回值
        /// </summary>
        
        new public T ReturnValue { get; set; }
    }
}
