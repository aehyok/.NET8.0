using Ardalis.Specification.EntityFrameworkCore;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using LinqKit.Core;

namespace sun.EntityFrameworkCore.Repository.Query
{
    /// <summary>
    ///  参考 https://github.com/TanvirArjel/EFCore.GenericRepository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class QueryRepository<TEntity, TKey> : IQueryRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly DbContext DbContext;
        private readonly ISpecificationEvaluator specificationEvaluator;

        public QueryRepository(DbContext dbContext)
            : this(dbContext, SpecificationEvaluator.Default)
        {
            DbContext = dbContext;
        }

        public QueryRepository(DbContext dbContext, ISpecificationEvaluator specificationEvaluator)
        {
            DbContext = dbContext;
            this.specificationEvaluator = specificationEvaluator;
        }

        public DbSet<TEntity> Entities
        {
            get
            {
                return DbContext.Set<TEntity>();
            }
        }

        public IQueryable<TEntity> GetExpandable()
        {
            return GetQueryable().AsExpandable();
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return Entities;
        }

        public virtual Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition = null,
                                                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
                                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orders = null,
                                                        bool asNoTracking = true,
                                                        CancellationToken cancellationToken = default)
        {
            var query = GetQueryable();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (orders != null)
            {
                query = orders(query);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return query.ToListAsync(cancellationToken);
        }

        public virtual Task<List<TEntity>> GetListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return ApplySpecification(specification).ToListAsync(cancellationToken);
        }

        public virtual Task<List<TResult>> GetListAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default)
        {
            return ApplySpecification(specification).ToListAsync(cancellationToken);
        }

        public virtual Task<IPagedList<TEntity>> GetPagedListAsync(ISpecification<TEntity> specification, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            return ApplySpecification(specification).ToPagedListAsync(pageIndex, pageSize, null, cancellationToken);
        }

        public virtual Task<IPagedList<TResult>> GetPagedListAsync<TResult>(ISpecification<TEntity, TResult> specification, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            return ApplySpecification(specification).ToPagedListAsync(pageIndex, pageSize, null, cancellationToken);
        }

        public virtual Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return GetByIdAsync(id, true, cancellationToken);
        }

        public virtual Task<TEntity> GetByIdAsync(object id, bool asNoTracking, CancellationToken cancellationToken = default)
        {
            return GetByIdAsync(id, null, asNoTracking, cancellationToken);
        }

        public virtual Task<TEntity> GetByIdAsync(object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes, CancellationToken cancellationToken = default)
        {
            return GetByIdAsync(id, includes, true, cancellationToken);
        }

        public virtual Task<TEntity> GetByIdAsync(object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes, bool asNoTracking, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(id);

            IEntityType entityType = DbContext.Model.FindEntityType(typeof(TEntity));

            string primaryKeyName = entityType.FindPrimaryKey().Properties.Select(p => p.Name).FirstOrDefault();
            Type primaryKeyType = entityType.FindPrimaryKey().Properties.Select(p => p.ClrType).FirstOrDefault();

            if (primaryKeyName == null || primaryKeyType == null)
            {
                throw new ArgumentException("Entity does not have any primary key defined", nameof(id));
            }

            object primayKeyValue = null;

            try
            {
                primayKeyValue = Convert.ChangeType(id, primaryKeyType, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                throw new ArgumentException($"You can not assign a value of type {id.GetType()} to a property of type {primaryKeyType}");
            }

            ParameterExpression pe = Expression.Parameter(typeof(TEntity), "entity");
            MemberExpression me = Expression.Property(pe, primaryKeyName);
            ConstantExpression constant = Expression.Constant(primayKeyValue, primaryKeyType);
            BinaryExpression body = Expression.Equal(me, constant);
            Expression<Func<TEntity, bool>> expressionTree = Expression.Lambda<Func<TEntity, bool>>(body, new[] { pe });

            IQueryable<TEntity> query = DbContext.Set<TEntity>();

            if (includes != null)
            {
                query = includes(query);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return query.FirstOrDefaultAsync(expressionTree, cancellationToken);
        }

        public virtual Task<TResult> GetByIdAsync<TResult>(object id, Expression<Func<TEntity, TResult>> selectExpression, CancellationToken cancellationToken = default) where TResult : class
        {
            ArgumentNullException.ThrowIfNull(id);
            ArgumentNullException.ThrowIfNull(selectExpression);

            IEntityType entityType = DbContext.Model.FindEntityType(typeof(TEntity));

            string primaryKeyName = entityType.FindPrimaryKey().Properties.Select(p => p.Name).FirstOrDefault();
            Type primaryKeyType = entityType.FindPrimaryKey().Properties.Select(p => p.ClrType).FirstOrDefault();

            if (primaryKeyName == null || primaryKeyType == null)
            {
                throw new ArgumentException("Entity does not have any primary key defined", nameof(id));
            }

            object primayKeyValue = null;

            try
            {
                primayKeyValue = Convert.ChangeType(id, primaryKeyType, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                throw new ArgumentException($"You can not assign a value of type {id.GetType()} to a property of type {primaryKeyType}");
            }

            ParameterExpression pe = Expression.Parameter(typeof(TEntity), "entity");
            MemberExpression me = Expression.Property(pe, primaryKeyName);
            ConstantExpression constant = Expression.Constant(primayKeyValue, primaryKeyType);
            BinaryExpression body = Expression.Equal(me, constant);
            Expression<Func<TEntity, bool>> expressionTree = Expression.Lambda<Func<TEntity, bool>>(body, new[] { pe });

            IQueryable<TEntity> query = DbContext.Set<TEntity>();

            return query.Where(expressionTree).Select(selectExpression).FirstOrDefaultAsync(cancellationToken);
        }

        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            return GetAsync(condition, null, true, cancellationToken);
        }

        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, bool asNoTracking, CancellationToken cancellationToken = default)
        {
            return GetAsync(condition, null, asNoTracking, cancellationToken);
        }

        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes, CancellationToken cancellationToken = default)
        {
            return GetAsync(condition, includes, true, cancellationToken);
        }

        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes, bool asNoTracking, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(condition);

            var query = GetQueryable().Where(condition);

            if (includes != null)
            {
                query = includes(query);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return query.FirstOrDefaultAsync(cancellationToken);
        }

        public virtual Task<TResult> GetAsync<TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> selectExpression, CancellationToken cancellationToken = default) where TResult : class
        {
            ArgumentNullException.ThrowIfNull(condition);
            ArgumentNullException.ThrowIfNull(selectExpression);

            return GetQueryable().Where(condition).Select(selectExpression).FirstOrDefaultAsync(cancellationToken);
        }

        public virtual Task<TEntity> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
        }

        public virtual Task<TResult> GetAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default)
        {
            return ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
        }

        public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(condition);
            return GetQueryable().Where(condition).AnyAsync(cancellationToken);
        }

        public virtual Task<bool> ExistsAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return ApplySpecification(specification).AnyAsync(cancellationToken);
        }

        public virtual Task<int> GetCountAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(condition);
            return GetQueryable().Where(condition).CountAsync(cancellationToken);
        }

        public virtual Task<int> GetCountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return ApplySpecification(specification).CountAsync(cancellationToken);
        }

        public virtual Task<long> GetLongCountAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(condition);
            return GetQueryable().Where(condition).LongCountAsync(cancellationToken);
        }

        public virtual Task<long> GetLongCountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return ApplySpecification(specification).LongCountAsync();
        }

        public virtual async Task<List<T>> GetFromRawSqlAsync<T>(string sql, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            IEnumerable<object> parameters = new List<object>();

            return await DbContext.GetFromQueryAsync<T>(sql, parameters, cancellationToken);
        }

        public virtual async Task<List<T>> GetFromRawSqlAsync<T>(string sql, object parameter, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            List<object> parameters = new List<object>() { parameter };
            return await DbContext.GetFromQueryAsync<T>(sql, parameters, cancellationToken);
        }

        public virtual async Task<List<T>> GetFromRawSqlAsync<T>(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            return await DbContext.GetFromQueryAsync<T>(sql, parameters, cancellationToken);
        }

        public virtual IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            ArgumentNullException.ThrowIfNull(specification);
            return specificationEvaluator.GetQuery(GetQueryable(), specification);
        }

        public virtual IQueryable<TResult> ApplySpecification<TResult>(ISpecification<TEntity, TResult> specification)
        {
            ArgumentNullException.ThrowIfNull(specification);
            return specificationEvaluator.GetQuery(GetQueryable(), specification);
        }

        public Task<IPagedList<TEntity>> GetPagedListAsync(int pageIndex = 1,
                                                               int pageSize = 10,
                                                               Expression<Func<TEntity, bool>> condition = null,
                                                               Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
                                                               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orders = null,
                                                               bool asNoTracking = true,
                                                               CancellationToken cancellationToken = default)
        {
            var query = GetQueryable();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (orders != null)
            {
                query = orders(query);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return query.ToPagedListAsync(pageIndex, pageSize, null, cancellationToken);
        }

        public Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selectExpression,
                                                                        int pageIndex = 1,
                                                                        int pageSize = 10,
                                                                        Expression<Func<TEntity, bool>> condition = null,
                                                                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
                                                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orders = null,
                                                                        bool asNoTracking = true,
                                                                        CancellationToken cancellationToken = default) where TResult : class
        {
            ArgumentNullException.ThrowIfNull(selectExpression);
            var query = GetQueryable();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (orders != null)
            {
                query = orders(query);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return query.Select(selectExpression).ToPagedListAsync(pageIndex, pageSize, null, cancellationToken);
        }

        public Task<List<TResult>> GetListAsync<TResult>(Expression<Func<TEntity, TResult>> selectExpression, Expression<Func<TEntity, bool>> condition = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orders = null, bool asNoTracking = true, CancellationToken cancellationToken = default) where TResult : class
        {
            ArgumentNullException.ThrowIfNull(selectExpression);

            var query = GetQueryable();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (orders != null)
            {
                query = orders(query);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return query.Select(selectExpression).ToListAsync(cancellationToken);
        }
    }
}
