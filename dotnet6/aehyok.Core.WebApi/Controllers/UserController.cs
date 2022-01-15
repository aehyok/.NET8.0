using aehyok.Core.EntityFrameCore.MySql.Data;
using aehyok.Core.EntityFrameCore.MySql.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.WebApi.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public List<BaseUser> GetUserList(int pageIndex =0 )
        {
            try
            {
                var context = new MyDbConext();
                int pagesize = 10; 
                var list = context.BaseUsers.Where(item=> item.IsDeleted==false).OrderByDescending(item => item.CreatedAt).Skip(pagesize * pageIndex).Take(pagesize).ToList();
                this._logger.Info(list.Count);
                return list;
            }
            catch(Exception error)
            {
                this._logger.Error(error);
                return null;
            }
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<BaseUser> GetUser(int id)
        {
            try
            {
                var context = new MyDbConext();
                var user = await context.BaseUsers.FindAsync(id);
                return user;
            }
            catch (Exception error)
            {
                this._logger.Error(error);
                return null;
            }
        }

        /// <summary>
        ///保存用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> SaveUser(BaseUser user)
        {
            try
            {
                this._logger.Info(user.Id);
                if(user!=null  && user.Id > 0)
                {
                    var context = new MyDbConext();
                    user.CreatedAt = DateTime.Now;
                    user.IsDeleted = false;
                    user.UpdatedAt = DateTime.Now;
                    context.Update(user);
                    await context.SaveChangesAsync();
                    return true;
                } 
                else
                {
                    this._logger.Info(user);
                    var context = new MyDbConext();
                    user.IsDeleted = false;
                    user.CreatedAt= DateTime.Now;
                    user.UpdatedAt = DateTime.Now;
                    var result = await context.BaseUsers.AddAsync(user);
                    await context.SaveChangesAsync();
                    return true;
                }
                
            }catch(Exception error)
            {
                this._logger.Error(error);
                return false;
            }
        }



        /// <summary>
        ///删除用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> DeleteUser(int id)
        {
            var context = new MyDbConext();
            var user = await context.BaseUsers.FindAsync(id);
            user.IsDeleted = true;
            context.Update(user);
            context.SaveChanges();
            return true;
        }
    }
}
