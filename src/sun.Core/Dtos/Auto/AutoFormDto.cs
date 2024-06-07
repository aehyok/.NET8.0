using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Dtos.Auto
{
    /// <summary>
    /// form表单预览Dto
    /// </summary>
    public class AutoFormPreviewDto
    {
        /// <summary>
        /// 自动化任务名称
        /// </summary>
        public string AutoTaskName { get; set; }

        /// <summary>
        /// 自动化任务描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 任务附件列表
        /// </summary>
        public string AttachmentIds { get; set; }

        /// <summary>
        /// form表单元数据Id
        /// </summary>
        public string FormMetaDataId { get; set; }

        /// <summary>
        /// form表单元数据定义
        /// </summary>
        public string FormDefine { get; set; }
    }

    /// <summary>
    /// 自定义表单统一提交Dto
    /// </summary>
    public class AutoFormSubmitDto
    {
        /// <summary>
        /// form表单元数据定义Id
        /// </summary>
        public string FormMetaDataId { get; set; }

        public Dictionary<string, object> FormData { get; set; } = new Dictionary<string, object>();
    }

    public class AutoFormDto: AutoFormPreviewDto
    {
        /// <summary>
        /// 是否已经填报数据
        /// </summary>
        public bool IsReport { get; set; }

        /// <summary>
        /// form表单的数据
        /// </summary>
        public ExpandoObject FormData { get; set; }
    }
}
