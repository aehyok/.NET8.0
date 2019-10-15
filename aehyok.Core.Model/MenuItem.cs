using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace aehyok.Core.Model
{
    /// <summary>
    /// 菜单项定义
    /// </summary>
    public class MenuItem:BaseEntity
    {
        /// <summary>
		/// 菜单显示标题
		/// </summary>
        public string MenuTitle { get; set; }

        /// <summary>
        /// 菜单显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public string FatherId { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string IconName { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public string MenuType { get; set; }

        /// <summary>
        /// 菜单参数名称
        /// </summary>
        public string MenuParameter { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DefaultValue(true)]
        public bool Active { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 图标号
        /// </summary>
        public string IconIndex { get; set; }

        /// <summary>
        /// 标示菜单是否是注册菜单
        /// </summary>
        public bool IsRegister { set; get; }

        /// <summary>
        /// 子菜单列表
        /// </summary>
        public List<MenuItem> Children { get; set; }


        public MenuItem(string menuId, string menuTitle, string menuType, string menuParameter, int displayOrder, string fatherId, bool canUse, int level, string iconIndex, string iconName)
        {
            this.Id = menuId;
            this.MenuTitle = menuTitle;
            this.MenuType = menuType;
            this.MenuParameter = menuParameter;
            this.DisplayOrder = displayOrder;
            this.FatherId = fatherId;
            this.Active = canUse;
            this.Level = level;
            this.IconIndex = iconIndex;
            this.IconName = iconName;
            this.Children = new List<MenuItem>();
        }
    }
}
