using sun.Basic.Domains;
using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.NCDP.Domains
{
    /// <summary>
    /// 自定义表单定义
    /// </summary>
    public class AutoFormDefine: AuditedEntity
    {
        /// <summary>
        /// 表单的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表单结构定义
        /// </summary>
        public string InputDefine { get; set; }

        /// <summary>
        /// 子表单
        /// </summary>
        public string ChildrenInputDefine { get; set; }

        /// <summary>
        /// 表单新增时，初始化界面数据执行的SQL
        /// </summary>
        public string GetInitSQL { get; set; }

        /// <summary>
        /// 表单编辑时，初始化界面数据执行的SQL
        /// </summary>
        public string GetDataSQL { get; set; }

        /// <summary>
        /// 后置执行的SQL
        /// </summary>
        public string BehindSQL { get; set; }

        /// <summary>
        /// 写入数据的表
        /// </summary>
        public string WriteTables { get; set; }

        /// <summary>
        /// 表定义
        /// </summary>
        public string TableDefine { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 本模型对应的业务类型
        /// </summary>
        public string BizType { get; set; }
    }
}
