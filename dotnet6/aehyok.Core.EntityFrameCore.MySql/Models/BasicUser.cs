using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public partial class BasicUser
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户账号，兼容微信id
        /// </summary>
        public string Account { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string NickName { get; set; } = null!;
        /// <summary>
        /// 男性1，女性2，未知0
        /// </summary>
        public bool Sex { get; set; }
        public string Mobile { get; set; } = null!;
        public string Email { get; set; } = null!;
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; } = null!;
        /// <summary>
        /// 所属区域
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 所属部门Id
        /// </summary>
        public string DepartmentIds { get; set; } = null!;
        /// <summary>
        /// 角色id
        /// </summary>
        public string RoleIds { get; set; } = null!;
        /// <summary>
        /// 公众1村委2政务3企业4
        /// </summary>
        public sbyte Type { get; set; }
        /// <summary>
        /// 户籍人口表Id
        /// </summary>
        public int PopulationId { get; set; }
        /// <summary>
        /// 公众用户是否已认证，0未审核， 1待审核，2审核通过，3审核不通过
        /// </summary>
        public int IsAuth { get; set; }
        /// <summary>
        /// 是否主管人员
        /// </summary>
        public sbyte IsLeader { get; set; }
        public bool? Status { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime LoginedAt { get; set; }
        /// <summary>
        /// 园区id
        /// </summary>
        public int ParkAreaId { get; set; }
        /// <summary>
        /// 头像id
        /// </summary>
        public long PortraitFileId { get; set; }
        /// <summary>
        /// 户码Id
        /// </summary>
        public int HouseholdId { get; set; }
        /// <summary>
        /// 小程序openid
        /// </summary>
        public string Wxopenid { get; set; } = null!;
        /// <summary>
        /// 描述/职务
        /// </summary>
        public string Description { get; set; } = null!;
        /// <summary>
        /// 是否网格员，0否， 1一级网格员，2二级网格员(网格长)
        /// </summary>
        public int IsGrid { get; set; }
    }
}
