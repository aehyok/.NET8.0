using sun.Core.Domains;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure;
using sun.Serilog;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace sun.Core.Services
{
    public class OperationLogService(DbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, IMenuService menuService, IServiceProvider scopeFactory) : ServiceBase<OperationLog>(dbContext, mapper), IOperationLogService, IScopedDependency
    {
        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="code">操作菜单</param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task LogAsync(string code, string content, string json, string ipAddress, string userAgent, long userId =0)
        {
            using var scope = scopeFactory.CreateScope();
            var menuService = scope.ServiceProvider.GetService<IMenuService>();
            var operationLogService = scope.ServiceProvider.GetService<IServiceBase<OperationLog>>();
            
            var menus = await menuService.GetParentMenuAsync(code);
            if (!menus.Any())
            {
                return;
            }

            var operationMenu = string.Join(">", menus.OrderBy(a => a.IdSequences.Length).Select(a => a.Name));


            var entity = new OperationLog
            {
                IpAddress = ipAddress,
                OperationMenu = operationMenu,
                OperationContent = content,
                UserAgent = userAgent,
                MenuCode = code,
                Remark = json,
                CreatedBy = userId,
                UpdatedBy = userId
            };

            await operationLogService.InsertAsync(entity);
        }
    }
}
