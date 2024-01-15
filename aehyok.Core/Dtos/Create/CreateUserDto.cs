using aehyok.Core.Domains;
using System.ComponentModel.DataAnnotations;

namespace aehyok.Core.Dtos.Create
{
    public class CreateUserDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(64, ErrorMessage = "账号不能超过 64 个字符")]
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [MaxLength(64, ErrorMessage = "姓名不能超过 64 个字符")]
        public string RealName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public List<CreateUserRoleDto> Roles { get; set; }

        /// <summary>
        /// 人员所属区域
        /// </summary>
        public long RegionId { get; set; }
        /// <summary>
        /// 用户部门
        /// </summary>
        //public List<CreateUserDepartmentModel> Departments { get; set; }
    }

}
