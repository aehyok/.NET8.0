using aehyok.Basic.Domains;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure;
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

    }
}
