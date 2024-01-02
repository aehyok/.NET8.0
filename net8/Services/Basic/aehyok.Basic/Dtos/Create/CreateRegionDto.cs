using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Dtos.Create
{
    public class CreateRegionDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "区域名称不能为空")]
        [MaxLength(64, ErrorMessage = "区域名称不能超过 64 个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "区域代码不能为空")]
        [MaxLength(64, ErrorMessage = "区域代码不能超过 64 个字符")]
        public string Code { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 父级编号
        /// </summary>
        public long ParentId { get; set; }
    }
}
