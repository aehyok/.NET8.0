using sun.EntityFrameworkCore.Repository;
using sun.Core.Domains;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sun.Infrastructure;
using sun.Core.Domains.Auto;

namespace sun.NCDP.Services
{
    public class TaskService : ServiceBase<AutoTask>, ITaskService, IScopedDependency
    {
        public TaskService(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
