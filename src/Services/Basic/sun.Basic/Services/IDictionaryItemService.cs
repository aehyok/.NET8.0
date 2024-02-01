using sun.Basic.Domains;
using sun.Basic.Dtos;
using sun.EntityFrameworkCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Basic.Services
{
    /// <summary>
    ///字典项接口
    /// </summary>
    public interface IDictionaryItemService: IServiceBase<DictionaryItem>
    {
        /// <summary>
        /// 获取字典项树列表
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<DictionaryItemDto>> GetTreeListAsync(long groupId, string keyword = "");
    }
}
