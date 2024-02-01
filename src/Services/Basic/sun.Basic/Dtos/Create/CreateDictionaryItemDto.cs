using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Basic.Dtos.Create
{
    public class CreateDictionaryItemDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "字典项名称不能为空")]
        [StringLength(64, MinimumLength = 1, ErrorMessage = "字典项名称不能小于 1 个字符，大于 64 个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "字典编码不能为空")]
        [StringLength(64, MinimumLength = 1, ErrorMessage = "字典项名称不能小于 1 个字符，大于 64 个字符")]
        public string Code { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 父级编号
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 字典分组编号
        /// </summary>
        public long DictionaryGroupId { get; set; }

        /// <summary>
        /// 分组代码
        /// </summary>
        public string DictionaryGroupCode { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; } = true;

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
