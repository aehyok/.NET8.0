using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 代码表数据
    /// 
    /// Create by:Lintx
    /// </summary>
    public class MD_RefTable_Data
    {
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        ///  代码显示名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 代码拼音字头
        /// </summary>
        public string Pyzt { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public string DisplayOrder { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsUsed { get; set; }
        /// <summary>
        /// 备注 
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 父节点  （此值为空时则为根节点）
        /// </summary>
        public string FatherCode { get; set; }
        /// <summary>
        /// 是否可以用于显示
        /// </summary>
        public bool CanDisplay { get; set; }
        /// <summary>
        /// 是否可用于输入
        /// </summary>
        public bool CanInput { get; set; }
        /// <summary>
        /// 本节点是否是叶子节点
        /// </summary>
        public bool IsLeaf { get; set; }

    }
}
