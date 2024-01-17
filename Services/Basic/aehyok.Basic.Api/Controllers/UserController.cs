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
using aehyok.Core.Dtos.Query;
using aehyok.Core.Domains;
using aehyok.Core.Dtos.Create;
using aehyok.Core.Dtos;
using aehyok.Core.Services;
using aehyok.Infrastructure.Enums;
using LinqKit;
using X.PagedList.EF;
using aehyok.Infrastructure.Utils;

namespace aehyok.Basic.Api.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserController(
        IUserService userService) : BasicControllerBase
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IPagedList<UserDto>> GetListAsync([FromQuery] UserQueryDto model)
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

            var list = await userService.GetPagedListAsync<UserDto>(spec, model.Page, model.Limit);

            foreach(var item in list)
            { 
                if(item.Roles is not null && item.Roles.Count > 0)
                    item.Roles.OrderBy(a => a.PlatformType);
            }
            return list;
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
