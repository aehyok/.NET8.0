using aehyok.EntityFramework.Repository;
using aehyok.EntityFramework;
using aehyok.NCDP.Domains;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AutoTask = aehyok.NCDP.Domains.AutoTask;
using aehyok.Core;

namespace aehyok.NCDP.Services
{
    public class TaskService : ServiceBase<AutoTask>, ITaskService, IScopedDependency
    {
        public TaskService(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
