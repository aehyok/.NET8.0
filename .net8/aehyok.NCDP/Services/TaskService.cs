using aehyok.EntityFramework.Repository;
using aehyok.EntityFramework;
using aehyok.NCDP.Domains;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Task = aehyok.NCDP.Domains.Task;
using aehyok.Core;

namespace aehyok.NCDP.Services
{
    public class TaskService : ServiceBase<Task>, ITaskService, IScopedDependency
    {
        public TaskService(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
