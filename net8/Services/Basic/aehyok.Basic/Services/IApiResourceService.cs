using aehyok.Basic.Dtos;
using aehyok.Core.Domains;
using aehyok.EntityFramework.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Services
{
    public interface IApiResourceService: IServiceBase<ApiResource>
    {
        /// <summary>
        /// 获取接口资源定义树列表
        /// </summary>
        /// <returns></returns>
        Task<List<MenuResourceDto>> GetTreeListAsync();
    }
}
