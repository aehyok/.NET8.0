using aehyok.EntityFramework;
using aehyok.EntityFramework.Repository;
using aehyok.NCDP.Domains;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = aehyok.NCDP.Domains.Task;

namespace aehyok.NCDP.Services
{
    public class TaskService : ServiceBase<Task>, ITaskService, IScopedDependency
    {
        public TaskService(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
