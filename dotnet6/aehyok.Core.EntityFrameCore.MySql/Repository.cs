using System;
using aehyok.Base;
using aehyok.Core.EntityFrameCore.MySql.Data;
using Microsoft.EntityFrameworkCore;

namespace aehyok.Core.EntityFrameCore.MySql
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        MyDbContext EF = new MyDbContext();

        public DbSet<TEntity> Entities
        {
            get { return EF.Set<TEntity>(); }
        }

        DbSet<TEntity> IRepository<TEntity>.Entities => throw new NotImplementedException();

        public async Task<int> Insert(TEntity entity)
        {
            Entities.Add(entity);
            return await EF.SaveChangesAsync();
        }

        public async Task<int> Insert(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
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
            var item = await Entities.FindAsync(id);
            if(item != null)
            {
                EF.Remove<TEntity>(item);
                return await EF.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<TEntity> GetByKey(object key)
        {
            return await Entities.FindAsync(key);
        }
    }
}

