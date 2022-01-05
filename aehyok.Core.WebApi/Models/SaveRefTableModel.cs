using aehyok.Lib.MetaData.Define;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Base.Models
{
    /// <summary>
    /// 保存代码表模型
    /// </summary>
    public class SaveRefTableModel
    {
        /// <summary>
        /// 代码表定义
        /// </summary>
        public MD_RefTable RefTable { get; set; }

        /// <summary>
        /// 代码表数据
        /// </summary>
        public DataTable RefData { get; set; }

    }
}
