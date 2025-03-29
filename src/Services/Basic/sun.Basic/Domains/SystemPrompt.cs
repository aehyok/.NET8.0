using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Basic.Domains
{
    public class SystemPrompt : AuditedEntity
    {
        /// <summary>
        /// 提示词标题
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 提示词内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public SystemPromptType SystemPromptType { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
    }

    public enum SystemPromptType
    {
        公众号封面图 = 1,
        公众号卡片 = 2
    }
}
