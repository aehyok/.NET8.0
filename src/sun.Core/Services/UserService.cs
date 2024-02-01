using sun.Basic.Services;
using sun.Core.Domains;
using sun.EntityFrameworkCore.Repository;
using sun.Excel.Import;
using sun.Infrastructure;
using sun.Infrastructure.Enums;
using sun.Infrastructure.Exceptions;
using sun.Infrastructure.Options;
using sun.Infrastructure.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringExtensions = sun.Infrastructure.Utils.StringExtensions;

namespace sun.Core.Services
{
    public class UserService(DbContext dbContext, IMapper mapper, IFileService fileService, IRegionService regionService, IRoleService roleService) : ServiceBase<User>(dbContext, mapper), IUserService, IScopedDependency
    {
        public async Task<dynamic> ImportAsync(string url, UserType userType, long operatorId)
        {
            //// 获取文件的本地路径
            var localPath = await fileService.GetTempFilePathAsync(url);

            var reader = new ExcelReader(localPath);

            // 加载导入表字段映射关系
            reader.LoadMapConfiguration(Path.Combine(AppContext.BaseDirectory, "Templates/UserImportMapping.json"));

            var regions = await regionService.GetListAsync();
            var roles = await roleService.GetListAsync();

            var rows = reader.ReadAllRows(2, 1, 0, row =>
            {
                foreach (var cell in row.Cells)
                {

                    switch (cell.Name)
                    {
                        case "Mobile":
                            if (!cell.Value.IsMobile())
                            {
                                row.Errors.Add("手机号码不正确");
                            }
                            break;

                        case "RegionCode1":
                        case "RegionCode2":
                        case "RegionCode3":
                            if (!cell.Value.IsNullOrEmpty())
                            {
                                var region = regions.FirstOrDefault(a => a.Code == cell.Value);
                                if (region is null)
                                {
                                    row.Errors.Add($"区域代码不正确");
                                }
                                else
                                {
                                    cell.Value = region.Id.ToString();
                                }
                            }
                            break;

                        case "RoleCode1":
                        case "RoleCode2":
                        case "RoleCode3":
                            if (!cell.Value.IsNullOrEmpty())
                            {
                                var role = roles.FirstOrDefault(a => a.Code == cell.Value);
                                if (role is null)
                                {
                                    row.Errors.Add($"角色代码不正确");
                                }
                                else
                                {
                                    cell.Value = role.Id.ToString();
                                }
                            }
                            break;
                    }
                }
            });

            foreach (var row in rows.Where(a => a.Errors.Count == 0))
            {
                try
                {
                    var user = new User()
                    {
                        Avatar = string.Empty,
                        CreatedBy = operatorId,
                        Email = string.Empty,
                        NickName = string.Empty,
                        //PopulationId = 0,
                        UpdatedBy = operatorId,
                        IsEnable = true,
                        Mobile = row.GetValue<string>("Mobile"),
                        RealName = row.GetValue<string>("RealName"),
                        UserName = row.GetValue<string>("UserName")
                    };

                    if (Enum.TryParse<Gender>(row.GetValue<string>("Gender"), true, out var gender))
                    {
                        user.Gender = gender;
                    }
                    else
                    {
                        user.Gender = Gender.未知;
                    }

                    user.Password = user.Mobile[^6..];

                    var userRoles = new List<UserRole>();

                    foreach (var i in Enumerable.Range(1, 3))
                    {
                        if (!row.GetValue<string>($"RegionCode{i}").IsNullOrEmpty() && !row.GetValue<string>($"RoleCode{i}").IsNullOrEmpty())
                        {
                            userRoles.Add(new UserRole
                            {
                                RoleId = row.GetValue<long>($"RoleCode{i}"),
                                RegionId = row.GetValue<long>($"RegionCode{i}"),
                                UserId = user.Id
                            });
                        }
                    }

                    user.UserRoles = userRoles;

                    await this.InsertAsync(user);
                }
                catch (Exception ex)
                {
                    row.Errors.Add(ex.Message);
                }
            }

            var result = new
            {
                Total = rows.Count,
                Successed = rows.Count(a => a.Errors.Count == 0),
                Failed = rows.Count(a => a.Errors.Count > 0),
                FailedData = rows.Where(a => a.Errors.Count > 0).Select(a => new
                {
                    RealName = a.GetValue<string>("RealName"),
                    Mobile = a.GetValue<string>("Mobile"),
                    Errors = a.Errors
                })
            };

            return result;
        }

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
