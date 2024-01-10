using aehyok.Basic.Dtos;
using aehyok.Core.Domains;
using aehyok.Core.Dtos;
using aehyok.EntityFrameworkCore.Repository;

namespace aehyok.Core.Services
{
    public interface IApiResrouceCoreService : IServiceBase<ApiResource>
    {
        /// <summary>
        /// 获取接口资源定义树列表
        /// </summary>
        /// <returns></returns>
        Task<List<MenuResourceDto>> GetTreeListAsync();
    }
}
