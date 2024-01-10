using aehyok.Core.Domains;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure;
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
    public class UserRoleService(DbContext dbContext, IMapper mapper) : ServiceBase<UserRole>(dbContext, mapper), IUserRoleService, IScopedDependency
    {
        public Task<UserRole> GetUserDefaultRole(long userId)
        {
            var spec = Specifications<UserRole>.Create();
            spec.Query.Where(a => a.UserId == userId).OrderByDescending(a => a.IsDefault).ThenBy(a => a.Id);
            return this.GetAsync(spec);
        }
    }
}
