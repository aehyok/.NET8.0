namespace aehyok.Core.Dtos
{
    public class ChangeRolePermissionDto
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public List<ChangePermissionModel> Premission { get; set; } = new List<ChangePermissionModel>();
    }

    public class ChangePermissionModel
    {
        /// <summary>
        /// 菜单 Id
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 是否拥有权限
        /// </summary>
        public bool HasPermission { get; set; }

        ///// <summary>
        ///// 数据范围
        ///// </summary>
        //public DataRange DataRange { get; set; }
    }
}
