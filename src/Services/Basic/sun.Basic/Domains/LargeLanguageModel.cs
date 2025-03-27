using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Basic.Domains
{
    /// <summary>
    /// 大语言模型
    /// </summary>
    public class LargeLanguageModel : AuditedEntity
    {
        /// <summary>
        /// 模型名称
        /// </summary>
        [MaxLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// 模型代码
        /// </summary>
        [MaxLength(64)]
        public string Code { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [MaxLength(64)]
        public string DisplayName { get; set; }

        /// <summary>
        /// 模型图标
        /// </summary>
        [MaxLength(512)]
        public string Icon { get; set; }

        /// <summary>
        /// ApiKey
        /// </summary>
        [MaxLength(128)]
        public string ApiKey { get; set; }

        /// <summary>
        /// 接口 Url
        /// </summary>
        [MaxLength(512)]
        public string BaseUrl { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 系统提示词
        /// </summary>
        [MaxLength(1024)]
        public string SystemPrompt { get; set; }
    }

}
