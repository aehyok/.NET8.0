using aehyok.Core.Domains;
using aehyok.Core.Dtos;
using aehyok.Core.Dtos.Create;
using aehyok.Core.Dtos.Query;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure.Enums;


namespace aehyok.Core.Services
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
    }
}
