using Ardalis.Specification.EntityFrameworkCore;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using sun.EntityFrameworkCore.Repository.Query;

namespace sun.EntityFrameworkCore.Repository.Base
{
    public abstract class RepositoryBase<TEntity, TKey>(
        DbContext dbContext, 
        ISpecificationEvaluator specificationEvaluator) : QueryRepository<TEntity, TKey>(dbContext, specificationEvaluator), IRepositoryBase<TEntity, TKey> where TEntity : class
    {
        public RepositoryBase(DbContext dbContext)
            : this(dbContext, SpecificationEvaluator.Default)
        {
        }

        public virtual async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Unspecified, CancellationToken cancellationToken = default)
        {
            IDbContextTransaction dbContextTransaction = await DbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
            return dbContextTransaction;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(nameof(entity));

            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return entity;
        }

        public virtual async Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(nameof(entities));

            await DbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(nameof(entity));

            Entities.Update(entity);
            return DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual Task<int> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(nameof(entities));

            Entities.UpdateRange(entities);
            return DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(nameof(entity));

            DbContext.Set<TEntity>().Remove(entity);
            return DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual Task<int> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(nameof(entities));

            DbContext.Set<TEntity>().RemoveRange(entities);
            return DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken = default)
        {
            return DbContext.Database.ExecuteSqlRawAsync(sql, cancellationToken);
        }

        public virtual Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return DbContext.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public virtual Task<int> ExecuteSqlCommandAsync(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default)
        {
            return DbContext.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
        }

        public void ResetContextState()
        {
            DbContext.ChangeTracker.Clear();
        }
    }
}
