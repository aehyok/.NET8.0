using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
using aehyok.Basic.Dtos.Query;
using aehyok.Basic.Services;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure.Exceptions;
using Ardalis.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using aehyok.Infrastructure;
using aehyok.Core.Dtos.Query;
using aehyok.Core.Domains;
using aehyok.Core.Dtos.Create;
using aehyok.Core.Dtos;
using aehyok.Core.Services;
using aehyok.Infrastructure.Enums;

namespace aehyok.Basic.Api.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserController(
        IUserService userService, 
        IPermissionService permissionService, 
        IMenuService menuService,
        IUserRoleService userRoleService) : BasicControllerBase
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="roleService"></param>
        /// <param name="regionService"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IPagedList<UserDto>> GetListAsync([FromQuery] UserQueryDto model
            , [FromServices] IRoleService roleService
            , [FromServices] IRegionService regionService)
        {
            var spec = Specifications<User>.Create();
            spec.Query.OrderByDescending(a => a.Id).Include(a => a.Roles);

            if (!string.IsNullOrWhiteSpace(model.Keyword))
            {
                spec.Query.Search(a => a.UserName, $"%{model.Keyword}%")
                          .Search(a => a.Mobile, $"%{model.Keyword}%")
                          .Search(a => a.NickName, $"%{model.Keyword}%")
                          .Search(a => a.RealName, $"%{model.Keyword}%");
            }

            if (model.IsEnable.HasValue)
            {
                spec.Query.Where(a => a.IsEnable == model.IsEnable.Value);
            }

            if (model.RoleId.HasValue && model.RoleId != 0)
            {
                spec.Query.Where(a => a.UserRoles.Any(c => c.RoleId == model.RoleId.Value));
            }

            //if (model.RegionCode.IsNotNullOrEmpty())
            //{
            //    var regionInfo = await regionService.GetAsync(e => e.Code == model.RegionCode);
            //    if (regionInfo is null) throw new ErrorCodeException(-1, "未找到对应区域");
            //    model.RegionId = regionInfo.Id;
            //}

            
            // 所有包含农户角色和非游客角色之外角色信息的的用户
            if (model.RegionId.HasValue && model.RegionId != 0)
            {
                if (model.IncludeChilds)
                {
                    spec.Query.Where(a => a.UserRoles.Any(c => EF.Functions.Like(c.Region.IdSequences, $"%.{model.RegionId.Value}.%")));
                }
                else
                {
                    spec.Query.Where(a => a.UserRoles.Any(c => c.RegionId == model.RegionId.Value));
                }
            }

            return await userService.GetPagedListAsync<UserDto>(spec, model.Page, model.Limit);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> PostAsync(CreateUserDto model)
        {
            if (model.UserName.IsNullOrEmpty())
                throw new ErrorCodeException(-1, "账号不能为空");
            if (model.Mobile.IsNullOrEmpty())
                throw new ErrorCodeException(-1, "手机号码不能为空");
            if (model.Roles.IsNull())
                throw new ErrorCodeException(-1, "请为用户选择角色");

            var entity = this.Mapper.Map<User>(model);
            entity.UserRoles = model.Roles.Select(a => new UserRole
            {
                RoleId = a.RoleId,
                UserId = entity.Id,
                RegionId = a.RegionId
            }).ToList();

            //if (model.Departments.IsNotNull())// 插入新部门
            //    await userDepartmentService.InsertAsync(model.Departments.Select(a => new UserDepartment
            //    {
            //        UserId = entity.Id,
            //        RegionId = a.RegionId,
            //        DepartmentId = a.DepartmentId
            //    }).ToList());

            // 设置默认密码为手机号码后 6 位
            entity.Password = model.Mobile[^6..];
            await userService.InsertAsync(entity);
            return entity.Id;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpDelete("{id}")]
        public async Task<StatusCodeResult> DeleteAsync(long id)
        {
            var entity = await userService.GetAsync(a => a.Id == id);
            if (entity is null)
            {
                throw new Exception("你要删除的用户不存在");
            }

            await userService.DeleteAsync(entity);
            return Ok();
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<StatusCodeResult> PutAsync(long id, CreateUserDto model)
        {
            var entity = await userService.GetByIdAsync(id);
            if (entity is null)
            {
                throw new ErrorCodeException(-1, "你要修改的用户不存在");
            }

            entity = this.Mapper.Map(model, entity);

            entity.UserRoles = model.Roles.Select(a => new UserRole
            {
                RoleId = a.RoleId,
                UserId = entity.Id,
                RegionId = a.RegionId
            }).ToList();

            // 删除原部门
            //await userDepartmentService.GetQueryable().Where(a => a.UserId == entity.Id).UpdateFromQueryAsync(m => new UserDepartment() { IsDeleted = true });
            //if (model.Departments.IsNotNull())// 插入新部门
            //    await userDepartmentService.InsertAsync(model.Departments.Select(a => new UserDepartment
            //    {
            //        UserId = entity.Id,
            //        RegionId = a.RegionId,
            //        DepartmentId = a.DepartmentId
            //    }).ToList());

            await userService.UpdateAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("enable/{id}")]
        public async Task<StatusCodeResult> EnableAsync(long id)
        {
            var entity = await userService.GetByIdAsync(id);
            if (entity == null)
            {
                throw new ErrorCodeException(-1, "你要启用的数据不存在");
            }

            entity.IsEnable = true;

            await userService.UpdateAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("disable/{id}")]
        public async Task<StatusCodeResult> DisableAsync(long id)
        {
            var entity = await userService.GetByIdAsync(id);
            if (entity == null)
            {
                throw new ErrorCodeException(-1, "你要禁用的数据不存在");
            }

            entity.IsEnable = false;

            await userService.UpdateAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 获取当前用户权限
        /// </summary>
        /// <returns></returns>
        [HttpGet("permission")]
        public async Task<List<RolePermissionDto>> GetCurrentUserPermissionAsync(PlatformType platformType)
        {
            var currentUser = this.CurrentUser;

            var query = from p in permissionService.GetQueryable()
                        join m in menuService.GetQueryable() on p.MenuId equals m.Id
                        join ur in userRoleService.GetQueryable() on p.RoleId equals ur.RoleId
                        join r in userService.GetQueryable() on ur.UserId equals r.Id
                        where ur.UserId == currentUser.UserId  && m.PlatformType == platformType
                        select new RolePermissionDto
                        {
                            MenuId = m.Id,
                            RoleId = p.RoleId,
                            MenuName = m.Name,
                            MenuCode = m.Code,
                            ParentId = m.ParentId,
                            Order = m.Order
                        };
            var list = await query.ToListAsync();

            return list;
        }

        /// <summary>
        /// 获取当前登录用户详情
        /// </summary>
        /// <returns></returns>
        [HttpGet("current")]
        public async Task<CurrentUserDto> GetCurrentUserAsync()
        {
            //return await userService.GetCurrentUserAsync();
            return null;
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpPut("reset/{id}")]
        //public async Task<StatusCodeResult> ResetPasswordAsync(long id)
        //{
        //    await userService.ResetPasswordAsync(id);
        //    return Ok();
        //}
    }
}
