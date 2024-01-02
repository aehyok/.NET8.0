using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
using aehyok.Basic.Dtos.Create;
using aehyok.Basic.Dtos.Query;
using aehyok.EntityFramework.Repository;
using aehyok.Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Services
{
    public interface IMenuService: IServiceBase<Menu>
    {
        /// <summary>
        /// 获取菜单树（维护时）
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="includeChilds">是否包含下级</param>
        /// <returns></returns>
        Task<List<MenuTreeDto>> GetTreeListAsync(PlatformType platformType, MenuTreeQueryModel model);

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<long> PostAsync(CreateMenuDto model);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> PutAsync(long id, CreateMenuDto model);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(long id);
    }
}
