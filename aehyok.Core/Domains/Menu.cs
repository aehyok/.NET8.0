using aehyok.Infrastructure.Enums;
using aehyok.EntityFrameworkCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace aehyok.Core.Domains
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu : AuditedEntity
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        [MaxLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// 菜单标识
        /// </summary>
        [MaxLength(256)]
        public string Code { get; set; }

        /// <summary>
        /// 父级菜单
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public MenuType Type { get; set; }

        /// <summary>
        /// 所属平台类型
        /// </summary>
        public PlatformType PlatformType { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [MaxLength(1024)]
        public string Icon { get; set; }

        /// <summary>
        /// 菜单图标类型
        /// </summary>
        public MenuIconType IconType { get; set; }

        /// <summary>
        /// 选中图标
        /// </summary>
        [MaxLength(1024)]
        public string ActiveIcon { get; set; }

        /// <summary>
        /// 菜单选中图标类型
        /// </summary>
        public MenuIconType ActiveIconType { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        [MaxLength(1024)]
        public string Url { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// 是否外链
        /// </summary>
        public bool IsExternalLink { get; set; }

        /// <summary>
        /// Id 序列
        /// </summary>
        [MaxLength(1024)]
        public string IdSequences { get; set; }

        /// <summary>
        /// 下级菜单
        /// </summary>
        public virtual List<Menu> Children { get; set; }

        /// <summary>
        /// 父级菜单
        /// </summary>
        public virtual Menu Parent { get; set; }

        /// <summary>
        /// 菜单接口
        /// </summary>
        public virtual IEnumerable<MenuResource> Resources { get; set; }

        /// <summary>
        /// 所属系统Id
        /// </summary>
        public long SystemId { get; set; } = 0;
    }


    /// <summary>
    /// 菜单类型
    /// </summary>
    public enum MenuType
    {
        目录 = 0,
        菜单 = 1,
        操作 = 2,
    }

    /// <summary>
    /// 菜单图标类型
    /// </summary>
    public enum MenuIconType
    {
        图标 = 0,
        图片 = 1
    }
}
