using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Dtos
{
    public class CreateDictionaryGroupModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "分组名称不能为空")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "分组名称不能小于 2 个字符大于 100 个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "分组代码标识不能为空")]
        [MaxLength(4, ErrorMessage = "字典分组代码只能为 4 个字符")]
        public string Code { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
