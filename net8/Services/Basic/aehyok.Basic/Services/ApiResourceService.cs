using aehyok.Basic.Dtos;
using aehyok.Core.Domains;
using aehyok.EntityFramework.Repository;
using aehyok.Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Services
{
    public class ApiResourceService(DbContext dbContext, IMapper mapper) : ServiceBase<ApiResource>(dbContext, mapper), IApiResourceService, IScopedDependency
    {
        public async Task<List<MenuResourceDto>> GetTreeListAsync()
        {
            var resources = await GetListAsync();
            return resources.GroupBy(a => new { a.NameSpace, a.ControllerName, a.GroupName }).OrderBy(a => a.Key.NameSpace).Select(a =>
            {
                var resource = new MenuResourceDto
                {
                    Name = a.Key.GroupName,
                    Code = $"{a.Key.NameSpace}.{a.Key.ControllerName}",
                };

                resource.Operations = a.Select(c => this.Mapper.Map<MenuResourceDto>(c)).ToList();

                return resource;
            }).ToList();
        }
    }
}
