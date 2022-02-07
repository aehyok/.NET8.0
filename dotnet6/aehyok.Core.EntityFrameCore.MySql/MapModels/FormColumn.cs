using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.EntityFramework.MySql.MapModels
{
    public class FormColumn
    {
        /// <summary>
        /// 表单字段列表ID
        /// </summary>
        public string? Id { get; set; }  

        /// <summary>
        /// 表单name字段（与后台对接字段）
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 前端展示的字段名称
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 是否必填字段
        /// </summary>
        public bool? Required { get; set; }

        /// <summary>
        /// 字符串最大长度
        /// </summary>
        public int? Maxlength { get; set; }

        /// <summary>
        /// 是否
        /// </summary>
        public bool? Showwordlimit { get; set; }

    }
}
