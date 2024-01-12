using aehyok.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace aehyok.Core.Dtos.Create
{
    public class CreateRoleDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "角色名称不能为空")]
        [MaxLength(64, ErrorMessage = "角色名称不能超过 64 个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "角色代码不能为空")]
        [MaxLength(64, ErrorMessage = "角色代码不能超过 256 个字符")]
        public string Code { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 所属平台
        /// </summary>
        public PlatformType platformType { get; set; }
    }
}
