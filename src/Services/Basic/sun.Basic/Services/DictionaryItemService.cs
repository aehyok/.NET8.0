using sun.Basic.Domains;
using sun.Basic.Dtos;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure;
using sun.Infrastructure.Utils;
using Ardalis.Specification;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Basic.Services
{
    /// <summary>
    /// 字典项服务
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="mapper"></param>
    public class DictionaryItemService(DbContext dbContext, IMapper mapper) : ServiceBase<DictionaryItem>(dbContext, mapper), IDictionaryItemService,IScopedDependency
    {
        /// <summary>
        /// 获取字典项树
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        public virtual async Task<List<DictionaryItemDto>> GetTreeListAsync(long groupId, string keyword = "")
        {
            var spec = Specifications<DictionaryItem>.Create();
            spec.Query.OrderBy(a => a.Order).Include(c => c.Modifier);
            spec.Query.Where(a => a.DictionaryGroupId == groupId);

            if (!keyword.IsNullOrEmpty())
            {
                spec.Query.Search(a => a.Name, $"%{keyword}%");
            }

            var data = await this.GetListAsync(spec);

            return ParseTreeList(data, 0);
        }

        private List<DictionaryItemDto> ParseTreeList(List<DictionaryItem> data, long parentId)
        {
            return data.Where(a => a.ParentId == parentId).OrderBy(a => a.Order).Select(a =>
            {
                var item = this.Mapper.Map<DictionaryItemDto>(a);

                if (a.Modifier is not null)
                {
                    item.Modifier = a.Modifier.RealName;
                }

                item.Children = ParseTreeList(data, a.Id);

                return item;
            }).ToList();
        }
    }
}
