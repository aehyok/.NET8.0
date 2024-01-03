using aehyok.Basic.Domains;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure;
using aehyok.Infrastructure.Exceptions;
using aehyok.Infrastructure.Options;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Services
{
    public class UserService(DbContext dbContext, IMapper mapper) : ServiceBase<User>(dbContext, mapper), IUserService, IScopedDependency
    {
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task ResetPasswordAsync(long id)
        {
            var user = await this.GetByIdAsync(id);

            if (user == null)
            {
                throw new ErrorCodeException(100204, "用户不存在");
            }

            if (user.Mobile.IsNullOrEmpty())
            {
                throw new ErrorCodeException(100205, "请先为用户设置手机号码");
            }

            //await SendSetPasswordMessageAsync(user.Id, user.UserName, BasicConstants.MESSAGE_RESET_PASSWORD);
        }
    }
}
