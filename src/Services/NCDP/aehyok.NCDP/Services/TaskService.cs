using aehyok.EntityFrameworkCore.Repository;
using aehyok.NCDP.Domains;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using aehyok.Infrastructure;

namespace aehyok.NCDP.Services
{
    public class TaskService : ServiceBase<AutoTask>, ITaskService, IScopedDependency
    {
        public TaskService(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
