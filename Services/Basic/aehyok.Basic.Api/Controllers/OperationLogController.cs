using aehyok.Core;
using aehyok.Core.Domains;
using aehyok.Core.Dtos;
using aehyok.Core.Dtos.Query;
using aehyok.Core.Services;
using LinqKit;
using LinqKit.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace aehyok.Basic.Api.Controllers
{

    /// <summary>
    /// 操作日志
    /// </summary>
    public class OperationLogController(IOperationLogService operationLogService, IUserService userService) : BasicControllerBase
    {
        /// <summary>
        /// 操作日志列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IPagedList<OperationLogDto>> GetListAsync([FromQuery] OperationLogQueryDto model)
        {
            var filter = PredicateBuilder.New<OperationLog>(true);

            if (model.StartTime.HasValue)
            {
                filter.And( a => a.CreatedAt >= model.StartTime.Value);
            }

            if (model.EndTime.HasValue)
            {
                filter.And(a => a.CreatedAt <= model.EndTime.Value);
            }

            if (!string.IsNullOrWhiteSpace(model.Keyword))
            {
                filter.And(a => a.OperationContent.Contains(model.Keyword) || a.OperationMenu.Contains(model.Keyword));
            }

            var query = from log in operationLogService.GetExpandable().Where(filter)
                        join user in userService.GetQueryable() on log.CreatedBy equals user.Id into lu
                        from s in lu.DefaultIfEmpty()
                        select new OperationLogDto
                        {
                            Id = log.Id,
                            CreatedAt = log.CreatedAt,
                            CreatedBy = s.RealName ?? string.Empty,
                            OperationContent = log.OperationContent,
                            OperationMenu = log.OperationMenu
                        };
            query = query.OrderByDescending(a => a.Id);

           return await query.ToPagedListAsync(model.Page, model.Limit);
        }
    }
}
