using aehyok.Core.Domains;
using aehyok.EntityFrameworkCore.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Services
{
    public class AsyncTaskService(DbContext dbContext, IMapper mapper) : ServiceBase<AsyncTask>(dbContext, mapper), IAsyncTaskService
    {

    }
}
