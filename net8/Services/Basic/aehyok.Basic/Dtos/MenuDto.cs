using aehyok.Basic.Domains;
using aehyok.Infrastructure.Dtos;
using aehyok.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Dtos
{
    public class MenuDto : DtoBase
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        [MaxLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// 菜单标识
        /// </summary>
        [MaxLength(64)]
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
        /// 备注
        /// </summary>
        [MaxLength(1024)]
        public string Remark { get; set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// 是否外链
        /// </summary>
        public bool IsExternalLink { get; set; }

        /// <summary>
        /// 是否叶子节点
        /// </summary>
        public bool IsLeaf { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; }


        /// <summary>
        ///菜单所属平台类型
        /// </summary>
        public PlatformType PlatformType { get; set; }
    }
}
