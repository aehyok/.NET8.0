using sun.Basic.Dtos;
using sun.Core.Domains;
using sun.Core.Dtos;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace sun.Core.Services
{
    public class ApiResrouceCoreService(DbContext dbContext, IMapper mapper) : ServiceBase<ApiResource>(dbContext, mapper), IApiResrouceCoreService, IScopedDependency
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
