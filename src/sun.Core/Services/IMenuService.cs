using sun.Core.Domains;
using sun.Core.Dtos;
using sun.Core.Dtos.Create;
using sun.Core.Dtos.Query;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure.Enums;


namespace sun.Core.Services
{
    public interface IMenuService: IServiceBase<Menu>
    {
        /// <summary>
        /// 获取菜单树（维护时）
        /// </summary>
        /// <param name="platformType"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<List<MenuTreeDto>> GetTreeListAsync(PlatformType platformType, MenuTreeQueryDto model);

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

        /// <summary>
        /// 根据 Code 获取父级菜单
        /// </summary>
        /// <param name="menuCode></param>
        /// <returns></returns>
        Task<List<Menu>> GetParentMenuAsync(string menuCode);
    }
}
