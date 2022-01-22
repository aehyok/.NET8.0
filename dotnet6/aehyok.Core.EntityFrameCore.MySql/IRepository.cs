using System;
using Microsoft.EntityFrameworkCore;
using aehyok.Base;

namespace aehyok.Core.EntityFrameCore.MySql
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        DbSet<TEntity> Table { get; }

        IQueryable<TEntity> GetQueryable();

        List<TEntity> GetList();

        //增加单个实体
        Task<int> Insert(TEntity entity);

        //增加多个实体
        //int Insert(IEnumerable<TEntity> entities);

        //更新实体
        Task<int> Update(TEntity entity);

        //删除
        Task<int> Delete(object id);

        //根据逐渐获取实体
        Task<TEntity> GetByKey(object key);
    }
}

