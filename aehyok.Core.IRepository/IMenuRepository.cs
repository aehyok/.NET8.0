using aehyok.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.IRepository
{
    public interface IMenuRepository
    {
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        List<MenuItem> GetMenuList();
    }
}
