using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Basic.Dtos
{
    public class SystemPromptDto
    {
        /// <summary>
        /// 提示词名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 提示词内容
        /// </summary>
        public string Content { get; set; }
    }
}
