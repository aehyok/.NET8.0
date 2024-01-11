using aehyok.Core.Domains;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure;
using aehyok.Serilog;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Services
{
    public class OperationLogService : ServiceBase<OperationLog>, IOperationLogService, IScopedDependency
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMenuService menuService;

        public OperationLogService(DbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, IMenuService menuService)
            : base(dbContext, mapper)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.menuService = menuService;
        }

        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="code">操作菜单</param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task LogAsync(string code, string content, string json)
        {
            var menus = await this.menuService.GetParentMenuAsync(code);
            if (!menus.Any())
            {
                return;
            }

            var operationMenu = string.Join(">", menus.OrderBy(a => a.IdSequences.Length).Select(a => a.Name));

            var ipAddress = this.httpContextAccessor.HttpContext.Request.GetRemoteIpAddress();

            var entity = new OperationLog
            {
                IpAddress = ipAddress,
                OperationMenu = operationMenu,
                Operation = content,
                UserAgent = this.httpContextAccessor.HttpContext.Request.Headers.UserAgent,
                MenuCode = code,
                Remark = json
            };

            await this.InsertAsync(entity);
        }
    }
}
