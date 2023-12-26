using aehyok.Basic.Domains;
using aehyok.EntityFramework.Repository;
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
    public class ApiResrouceService(DbContext dbContext, IMapper mapper) : ServiceBase<ApiResource>(dbContext, mapper), IApiResrouceService, IScopedDependency
    {

    }
}
