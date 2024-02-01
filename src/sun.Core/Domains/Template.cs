using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Domains
{
    /// <summary>
    /// 模板
    /// </summary>
    public class Template : AuditedEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(120)]
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [MaxLength(256)]
        public string Code { get; set; }

        /// <summary>
        /// 内容类型
        /// </summary>
        public TemplateContentType ContentType { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [MaxLength(2048)]
        public string Content { get; set; }

        /// <summary>
        /// 变量
        /// </summary>
        public string Variable { get; set; }
    }

    /// <summary>
    /// 模板内容类型
    /// </summary>
    public enum TemplateContentType
    {
        文本 = 0,
        文件 = 1,
        小程序消息模板 = 2,
        公众号消息模板 = 3,
    }
}
