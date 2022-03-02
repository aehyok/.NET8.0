using aehyok.Base.Utils;
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
        public List<BaseUser> GetUserList()
        {
            try
            {
                var list = this._baseRepository.GetQueryable().Where(item => item.IsDeleted == false).ToList();
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
        ///添加新用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> AddUser(BaseUser user)
        {
            if (user == null)
            {
                return -1;
            }
            user.IsDeleted = false;
            user.Password = MD5Helper.MD5Encrypt32("123456" + "aehyok");  //默认密码
            user.CreatedAt = DateTime.Now;
            return await this._baseRepository.InsertAsync(user);
        }

        /// <summary>
        ///修改用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> UpdateUser(BaseUser user)
        {
            var updateUser = await this._baseRepository.GetByKey(user.Id);
            if (updateUser == null)
            {
                return -1;
            }
            user.IsDeleted = false;
            user.CreatedAt = updateUser.CreatedAt;
            user.Password = updateUser.Password;
            user.UpdatedAt = DateTime.Now;

            return await this._baseRepository.UpdateAsync(user);
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
