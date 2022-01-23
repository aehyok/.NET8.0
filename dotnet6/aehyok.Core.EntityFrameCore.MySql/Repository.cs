using System;
using System.Linq.Expressions;
using aehyok.Base;
using aehyok.Core.EntityFrameCore.MySql.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<int> Insert(TEntity entity)
        {
            Table.Add(entity);
            return await EF.SaveChangesAsync();
        }

        public async Task<int> Insert(IEnumerable<TEntity> entities)
        {
            Table.AddRange(entities);
            return await EF.SaveChangesAsync();
        }

        public async Task<int> Update(TEntity entity)
        {
            EF.Entry(entity).State = EntityState.Modified;
            return await EF.SaveChangesAsync();
        }

        public async Task<int> Delete(object id)
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


        public async Task<int> DeleteAsync(object id)
        {
            var entity = await this.GetAsync(id);
            return await this.DeleteAsync(entity);
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
    }
}

