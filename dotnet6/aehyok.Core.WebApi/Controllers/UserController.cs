using aehyok.Core.EntityFramework.MySql;
using aehyok.Core.EntityFramework.MySql.Data;
using aehyok.Core.EntityFramework.MySql.Models;
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
    public class UserController : BaseApiController
    {
        //private readonly IFlowRepository _flowRepository;
        private readonly IRepository<BaseUser> _baseRepository;

        public UserController(
            IRepository<BaseUser> baseRepository
            )
        {
            this._baseRepository = baseRepository;
        }
        public UserController()
        {

        }
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
                var context = new MyDbContext();
                int pagesize = 10; 
                var list = this._baseRepository.GetQueryable().Where(item => item.IsDeleted == false).OrderByDescending(item => item.CreatedAt).Skip(pagesize * pageIndex).Take(pagesize).ToList();
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
        public async Task<BaseUser> GetUser(string id)
        {
            try
            {
                var context = new MyDbContext();
                var user = await this._baseRepository.GetByKey(id);
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
                if(user!=null  && user.Id !=null)
                {
                    var context = new MyDbContext();
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
                    var context = new MyDbContext();
                    user.IsDeleted = false;
                    user.CreatedAt= DateTime.Now;
                    user.UpdatedAt = DateTime.Now;
                    var result = await this._baseRepository.InsertAsync(user);
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
        public async Task<bool> DeleteUser(string id)
        {
            var context = new MyDbContext();
            var user = await this._baseRepository.GetByKey(id);
            user.IsDeleted = true;
            context.Update(user);
            context.SaveChanges();
            return true;
        }
    }
}
