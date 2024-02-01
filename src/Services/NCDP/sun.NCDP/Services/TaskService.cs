using sun.EntityFrameworkCore.Repository;
using sun.NCDP.Domains;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sun.Infrastructure;

namespace sun.NCDP.Services
{
    public class TaskService : ServiceBase<AutoTask>, ITaskService, IScopedDependency
    {
        public TaskService(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
