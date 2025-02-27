using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sun.Basic.Domains;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Basic.Services
{
    public class WeChatBlogService(DbContext dbContext, IMapper mapper) : ServiceBase<WeChatBlog>(dbContext, mapper), IWeChatBlogService, IScopedDependency
    {

    }
}
