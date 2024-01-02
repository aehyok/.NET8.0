using aehyok.Core.Domains;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Services
{
    public class ApiResrouceCoreService(DbContext dbContext, IMapper mapper) : ServiceBase<ApiResource>(dbContext, mapper), IApiResrouceCoreService, IScopedDependency
    {

    }
}
