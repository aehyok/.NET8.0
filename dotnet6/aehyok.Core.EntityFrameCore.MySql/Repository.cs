using System;
using aehyok.Base;
using aehyok.Core.EntityFrameCore.MySql.Data;
using Microsoft.EntityFrameworkCore;

namespace aehyok.Core.EntityFrameCore.MySql
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        MyDbContext EF = new MyDbContext();

        public DbSet<TEntity> Table
        {
            get { return EF.Set<TEntity>(); }
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return this.Table.AsNoTracking();
        }

        public List<TEntity> GetList()
        {
            return this.GetQueryable().ToList();
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

        public async Task<TEntity> GetByKey(object key)
        {
            return await Table.FindAsync(key);
        }
    }
}

