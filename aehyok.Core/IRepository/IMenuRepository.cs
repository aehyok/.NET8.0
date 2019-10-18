using aehyok.Core.Data;
using aehyok.Core.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.IRepository
{
    public interface IMenuRepository: IDependency
    {
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        List<MenuItem> GetMenuList();
    }
}
