using sun.Basic.Dtos;
using sun.Core.Domains;
using sun.Core.Dtos;
using sun.EntityFrameworkCore.Repository;

namespace sun.Core.Services
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
