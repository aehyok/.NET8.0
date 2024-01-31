using aehyok.Core.Domains;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure;
using aehyok.Infrastructure.Enums;
using aehyok.Infrastructure.Exceptions;
using aehyok.Infrastructure.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Services
{
    public class RoleService(DbContext dbContext, IMapper mapper) : ServiceBase<Role>(dbContext, mapper), IRoleService, IScopedDependency
    {
        public override async Task<Role> InsertAsync(Role entity, CancellationToken cancellationToken = default)
        {
            if (entity.Code.IsNullOrEmpty())
            {
                throw new ErrorCodeException(-1, "角色代码不能为空");
            }

            if (await this.ExistsAsync(a => a.Code == entity.Code))
            {
                throw new ErrorCodeException(-1, "角色代码已存在");
            }

            return await base.InsertAsync(entity, cancellationToken);
        }

        public override async Task<int> UpdateAsync(Role entity, CancellationToken cancellationToken = default)
        {
            if(entity.IsSystem)
            {
                throw new ErrorCodeException(-1, "系统角色不允许修改");
            }
            if (entity.Code.IsNullOrEmpty())
            {
                throw new ErrorCodeException(-1, "角色代码不能为空");
            }

            if (await this.ExistsAsync(a => a.Code == entity.Code && a.Id != entity.Id))
            {
                throw new ErrorCodeException(-1, "角色代码已存在");
            }

            var result = await base.UpdateAsync(entity, cancellationToken);
            return result;
        }
    }
}
