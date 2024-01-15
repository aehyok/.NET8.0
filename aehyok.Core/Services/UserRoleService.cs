using aehyok.Basic.Services;
using aehyok.Core.Domains;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure;
using aehyok.Infrastructure.Enums;
using Ardalis.Specification;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Services
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public class UserRoleService(DbContext dbContext, IMapper mapper, IRoleService roleService) : ServiceBase<UserRole>(dbContext, mapper), IUserRoleService, IScopedDependency
    {
        public async Task ChangeDefaultRoleAsync(long userRoleId, long userId, PlatformType platformType)
        {
            var list = await this.GetUserRoles(userId, platformType);
            foreach(var userRole in list)
            {
                if (userRole.IsDefault)
                {
                    userRole.IsDefault = false;
                    await this.UpdateAsync(userRole);
                }

                if(userRole.Id == userRoleId)
                {
                    userRole.IsDefault = true;
                    await this.UpdateAsync(userRole);
                }
            }
        }

        public Task<UserRole> GetUserDefaultRole(long userId)
        {
            var spec = Specifications<UserRole>.Create();
            spec.Query.Where(a => a.UserId == userId).OrderByDescending(a => a.IsDefault).ThenBy(a => a.Id);
            return this.GetAsync(spec);
        }

        public async Task<List<UserRole>> GetUserRoles(long userId, PlatformType platformType)
        {
            var query = from ur in this.GetQueryable()
                        join r in roleService.GetQueryable() on ur.RoleId equals r.Id
                        where ur.UserId == userId && r.PlatformType == platformType
                        select ur;

            var list = await query.ToListAsync();

            return list;
        }
    }
}
