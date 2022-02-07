using System;
using Microsoft.EntityFrameworkCore;
using aehyok.Base;
using System.Linq.Expressions;
using X.PagedList;

namespace aehyok.Core.EntityFramework.MySql
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        DbContext EF { get; }
        DbSet<TEntity> Table { get; }

        IQueryable<TEntity> GetQueryable();

        List<TEntity> GetList();

        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);


        Task<int> InsertAsync(TEntity entity);

        Task<int> InsertRangeAsync(IEnumerable<TEntity> entities);

        Task<int> InsertRangeAsync(params TEntity[] entities);


        Task<int> UpdateAsync(TEntity entity);

        Task<int> UpdateRangeAsync(params TEntity[] entities);

        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities);

        //删除
        Task<int> DeleteAsync(object id);

        //根据逐渐获取实体
        Task<TEntity> GetByKey(object key);

        Task<TEntity> GetAsync(object id);

        Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate, int page, int limit);

        Task<IPagedList<TEntity>> GetPagedListAsync<TOrder>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrder>> orderBy, int page, int limit = 10, bool asc = true);

    }
}

