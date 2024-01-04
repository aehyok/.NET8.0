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
using StringExtensions = aehyok.Infrastructure.StringExtensions;

namespace aehyok.Basic.Services
{
    public class UserService(DbContext dbContext, IMapper mapper) : ServiceBase<User>(dbContext, mapper), IUserService, IScopedDependency
    {
        public override async Task<User> InsertAsync(User entity, CancellationToken cancellationToken = default)
        {
            // 密码不为空的时候，加密密码
            if(entity.Password.IsNotNullOrEmpty())
            {
                // 为每个密码生成一个32位的唯一盐值
                entity.PasswordSalt = StringExtensions.GeneratePassworldSalt();

                entity.Password = StringExtensions.EncodePassword(entity.Password, entity.PasswordSalt);
            }

            if(entity.UserName.IsNotNullOrEmpty() && await this.ExistsAsync(item => item.UserName == entity.UserName))
            {
                throw new ErrorCodeException(100201, "此用户名已存在");
            }

            if(entity.Mobile.IsNotNullOrEmpty() && await this.ExistsAsync(item => item.Mobile == entity.Mobile))
            {
                throw new ErrorCodeException(100202, "此手机号码已存在");
            }

            if (!entity.Email.IsNullOrEmpty() && await this.ExistsAsync(a => a.Email == entity.Email))
            {
                throw new ErrorCodeException(100203, "此邮箱已存在");
            }

            await base.InsertAsync(entity, cancellationToken);

            // 发送设置密码的短信
            return entity;
        }

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
