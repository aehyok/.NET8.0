using aehyok.Lib.MetaData.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace aehyok.Base.Models
{
    /// <summary>
    /// 保存的TABLE模型
    /// </summary>
    public class SaveTableModel
    {
        /// <summary>
        /// TABLE定义
        /// </summary>
        public DB_TableMeta TableMeta { get; set; }

        /// <summary>
        /// 命名空间定义
        /// </summary>
        public MD_Namespace NameSpace { get; set; }

    }
}
