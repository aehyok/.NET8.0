using aehyok.Core.Domains;
using aehyok.Infrastructure.Dtos;

namespace aehyok.Core.Dtos
{
    public class UserDto : AuditedDtoBase
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 是否设置密码
        /// </summary>
        public bool HasPassword { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 人口 Id
        /// </summary>
        //public long PopulationId { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public List<UserRoleDto> Roles { get; set; }

        /// <summary>
        /// 用户部门
        /// </summary>
        //public List<UserDepartmentDto> Departments { get; set; }
    }

    public class CurrentUserDto : UserDto
    {
        /// <summary>
        /// 角色 Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色区域 Id
        /// </summary>
        public long RegionId { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionName { get; set; }

        public string[] RegionFullName { get; set; }

        /// <summary>
        /// 人口身份证号码
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 区域等级
        /// </summary>
        public RegionLevel RegionLevel { get; set; }

        /// <summary>
        /// 是否关注公众号
        /// </summary>
        //public bool IsSubscribed { get; set; }

        /// <summary>
        /// 签名url
        /// </summary>
        public string SignatureUrl { get; set; }
    }
}
