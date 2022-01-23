using System;
using System.Linq.Expressions;
using aehyok.Base;
using aehyok.Core.EntityFrameCore.MySql.Data;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace aehyok.Core.EntityFrameCore.MySql
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        MyDbContext EF = new MyDbContext();

        public DbSet<TEntity> Table
        {
            get { return EF.Set<TEntity>(); }
        }

        /// <summary>
        /// 获取详细错误信息并回滚
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private string GetFullErrorTextAndRollback(DbUpdateException exception)
        {
            var entries = this.EF.ChangeTracker.Entries().Where(a => a.State == EntityState.Added || a.State == EntityState.Modified).ToList();

            entries.ForEach(a => a.State = EntityState.Unchanged);

            this.EF.SaveChanges();

            return exception.ToString();
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return this.Table.AsNoTracking();
        }

        public List<TEntity> GetList()
        {
            return this.GetQueryable().ToList();
        }

        /// <summary>
        /// 获取对象集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.GetQueryable().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                await this.Table.AddAsync(entity);
                await EF.SaveChangesAsync();

                return entity;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(this.GetFullErrorTextAndRollback(ex), ex);
            }
        }

        public async Task<int> InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            try
            {
                await this.Table.AddRangeAsync(entities);
                return await EF.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(this.GetFullErrorTextAndRollback(ex), ex);
            }
        }

        public async Task<int> DeleteAsync(object id)
        {
            ///删除操作实现
            var item = await Table.FindAsync(id);
            if(item != null)
            {
                EF.Remove<TEntity>(item);
                return await EF.SaveChangesAsync();
            }
            return 0;
        }

        private async Task<int> DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                this.Table.Remove(entity);
                return await EF.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(this.GetFullErrorTextAndRollback(ex), ex);
            }
        }

        public async Task<TEntity> GetByKey(object key)
        {
            return await Table.FindAsync(key);
        }

        public async Task<TEntity> GetAsync(object id)
        {
            return await Table.FindAsync(id);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="page">分页索引</param>
        /// <param name="limit">分页大小</param>
        /// <returns></returns>
        public async Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate, int page, int limit)
        {
            return await this.GetQueryable().Where(predicate).ToPagedListAsync(page, limit);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public async Task<IPagedList<TEntity>> GetPagedListAsync<TOrder>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrder>> orderBy, int page, int limit = 10, bool asc = true)
        {
            var query = this.GetQueryable().Where(predicate);
            return asc ? await query.OrderBy(orderBy).ToPagedListAsync(page, limit) : await query.OrderByDescending(orderBy).ToPagedListAsync(page, limit);
        }

        public async Task<int> InsertRangeAsync(params TEntity[] entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            try
            {
                await this.Table.AddRangeAsync(entities);
                return await EF.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(this.GetFullErrorTextAndRollback(ex), ex);
            }
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                this.Table.Update(entity);
                return await EF.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(this.GetFullErrorTextAndRollback(ex), ex);
            }
        }

        public async Task<int> UpdateRangeAsync(params TEntity[] entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            try
            {
                this.Table.UpdateRange(entities);
                return await EF.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(this.GetFullErrorTextAndRollback(ex), ex);
            }
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            try
            {
                this.Table.UpdateRange(entities);
                return await EF.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(this.GetFullErrorTextAndRollback(ex), ex);
            }
        }
    }
}

