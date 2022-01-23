using System;
using Microsoft.EntityFrameworkCore;
using aehyok.Base;
using System.Linq.Expressions;
using X.PagedList;

namespace aehyok.Core.EntityFrameCore.MySql
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        DbSet<TEntity> Table { get; }

        IQueryable<TEntity> GetQueryable();

        List<TEntity> GetList();

        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);

        //增加单个实体
        Task<int> Insert(TEntity entity);

        //增加多个实体
        //int Insert(IEnumerable<TEntity> entities);

        //更新实体
        Task<int> Update(TEntity entity);

        //删除
        Task<int> Delete(object id);

        Task<int> DeleteAsync(object id);
        //根据逐渐获取实体
        Task<TEntity> GetByKey(object key);

        Task<TEntity> GetAsync(object id);

        Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate, int page, int limit);

        Task<IPagedList<TEntity>> GetPagedListAsync<TOrder>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrder>> orderBy, int page, int limit = 10, bool asc = true);

    }
}

