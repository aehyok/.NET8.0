﻿using sun.Core.Domains;
using sun.Core.Dtos;
using sun.Core.Dtos.Create;
using sun.Core.Dtos.Query;
using sun.Core.Services;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure;
using sun.Infrastructure.Enums;
using sun.Infrastructure.Exceptions;
using Ardalis.Specification;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace sun.Core.Services
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

        public async Task<List<MenuTreeDto>> GetTreeListAsync(PlatformType platformType, MenuTreeQueryDto model)
        {
            //if (!model.ParentCode.IsNullOrEmpty())
            //{
            //    var parent = await this.GetAsync(a => a.Code == model.ParentCode && a.PlatformType == platformType) ?? throw new ErrorCodeException(-1, $"未找到代码【{model.ParentCode}】对应菜单") ;
            //    model.ParentId = parent.Id;
            //}

            var spec = Specifications<Menu>.Create();

            spec.Query.Where(a => a.PlatformType == platformType);
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
                var children = menus.Where(a => a.ParentId == parentId).OrderBy(a => a.Order).ToList();
                return children.Select(a =>
                {
                    var dto = this.Mapper.Map<MenuTreeDto>(a);
                    if(model.IncludeChilds)
                    {
                        dto.Children = getChildren(a.Id);
                    }

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

        /// <summary>
        /// 根据 Code 获取父级菜单
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual async Task<List<Menu>> GetParentMenuAsync(string menuCode)
        {
            var menu = await this.GetAsync(a => a.Code == menuCode);
            return await GetParentMenuAsync(menu);
        }

        /// <summary>
        /// 根据菜单对象获取父级菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public virtual async Task<List<Menu>> GetParentMenuAsync(Menu menu)
        {
            if (menu == null)
            {
                return new List<Menu>();
            }

            var parentIds = menu.IdSequences.Split(".", StringSplitOptions.RemoveEmptyEntries).Select(a => Convert.ToInt64(a));
            var parents = await this.GetListAsync(a => parentIds.Contains(a.Id));
            return parents;
        }
    }
}
