using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
using aehyok.Basic.Dtos.Create;
using aehyok.Basic.Dtos.Query;
using aehyok.EntityFramework.Repository;
using aehyok.Infrastructure;
using aehyok.Infrastructure.Enums;
using aehyok.Infrastructure.Exceptions;
using Ardalis.Specification;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace aehyok.Basic.Services
{
    public class MenuService(DbContext dbContext, IMapper mapper) : ServiceBase<Menu>(dbContext, mapper), IMenuService, IScopedDependency
    {
        public async Task<int> DeleteAsync(long id)
        {
            var entity = await this.GetAsync(a => a.Id == id);
            return entity is null ? throw new Exception("你要删除的数据不存在") : await this.DeleteAsync(entity);
        }

        public override async Task<Menu> InsertAsync(Menu entity, CancellationToken cancellationToken = default)
        {
            if (entity.ParentId != 0)
            {
                var parent = await this.GetByIdAsync(entity.ParentId) ?? throw new Exception("父级菜单不存在");
                entity.IdSequences = $"{parent.IdSequences}{entity.Id}.";
            }
            else
            {
                entity.IdSequences = $".{entity.Id}.";
            }

            var exists = await this.GetAsync(a => a.Code == entity.Code);
            if (exists != null && exists.Id != entity.Id)
            {
                throw new ErrorCodeException(-1, "菜单代码已存在");
            }

            if (entity.Id == entity.ParentId)
            {
                throw new ErrorCodeException(-1, "菜单的父级菜单不能是自己");
            }

            return await base.InsertAsync(entity, cancellationToken);
        }

        public async Task<List<MenuTreeDto>> GetTreeListAsync(PlatformType platformType, MenuTreeQueryModel model)
        {
            if (!model.ParentCode.IsNullOrEmpty())
            {
                var parent = await this.GetAsync(a => a.Code == model.ParentCode && a.PlatformType == platformType) ?? throw new ErrorCodeException(-1, $"未找到代码【{model.ParentCode}】对应菜单") ;
                model.ParentId = parent.Id;
            }

            var spec = Specifications<Menu>.Create();

            if(model.ParentId != 0)
            {
                if(model.IncludeChilds)
                {
                    spec.Query.Search(a => a.IdSequences, $"%{model.ParentId}%");
                }
                else
                {
                    spec.Query.Where(a => a.ParentId == model.ParentId);
                }
            }

            var menus = await this.GetListAsync(spec);

            List<MenuTreeDto> getChildren(long parentId)
            {
                var children = menus.Where(a => a.ParentId == parentId && a.PlatformType == platformType).OrderBy(a => a.Order).ToList();
                return children.Select(a =>
                {
                    var dto = this.Mapper.Map<MenuTreeDto>(a);
                    dto.Children = getChildren(a.Id);

                    if (dto.Children.Count == 0)
                    {
                        dto.Children = null;
                    }

                    return dto;
                }).ToList();
            }

            return getChildren(model.ParentId);
        }

        public async Task<long> PostAsync(CreateMenuDto model)
        {
            var entity = this.Mapper.Map<Menu>(model);
            await this.InsertAsync(entity);
            return entity.Id;
        }

        public async Task<int> PutAsync(long id, CreateMenuDto model)
        {
            var entity = await this.GetAsync(a => a.Id == id) ?? throw new Exception("你要修改的数据不存在");
            entity = this.Mapper.Map(model, entity);

            return await this.UpdateAsync(entity);
        }
    }
}
