using Ardalis.Specification;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace aehyok.EntityFramework.Repository.Query
{
    /// <summary>
    /// Contains all the query methods. 
    /// 参考 https://github.com/TanvirArjel/EFCore.GenericRepository
    /// </summary>
    public interface IQueryRepository<TEntity, TKey> where TEntity : class
    {
        IQueryable<TResult> ApplySpecification<TResult>(ISpecification<TEntity, TResult> specification);

        IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification);

        /// <summary>
        /// Gets <see cref="IQueryable{T}"/> of the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>Returns <see cref="IQueryable{T}"/>.</returns>
        IQueryable<TEntity> GetQueryable();

        /// <summary>
        /// 使用LinqKit扩展方法，获取可扩展的IQueryable
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetExpandable();

        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition = null,
                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orders = null,
                                         bool asNoTracking = true,
                                         CancellationToken cancellationToken = default);

        Task<List<TResult>> GetListAsync<TResult>(Expression<Func<TEntity, TResult>> selectExpression,
                                                  Expression<Func<TEntity, bool>> condition = null,
                                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
                                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orders = null,
                                                  bool asNoTracking = true,
                                                  CancellationToken cancellationToken = default) where TResult : class;

        Task<List<TEntity>> GetListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        Task<List<TResult>> GetListAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default);

        Task<IPagedList<TEntity>> GetPagedListAsync(int pageIndex = 1,
                                                        int pageSize = 10,
                                                        Expression<Func<TEntity, bool>> condition = null,
                                                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
                                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orders = null,
                                                        bool asNoTracking = true,
                                                        CancellationToken cancellationToken = default);

        Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selectExpression,
                                                                 int pageIndex = 1,
                                                                 int pageSize = 10,
                                                                 Expression<Func<TEntity, bool>> condition = null,
                                                                 Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
                                                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orders = null,
                                                                 bool asNoTracking = true,
                                                                 CancellationToken cancellationToken = default) where TResult : class;

        Task<IPagedList<TEntity>> GetPagedListAsync(ISpecification<TEntity> specification, int pageIndex = 1, int pageSize = 10, CancellationToken cancellationToken = default);

        Task<IPagedList<TResult>> GetPagedListAsync<TResult>(ISpecification<TEntity, TResult> specification, int pageIndex = 1, int pageSize = 10, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <paramref name="id"/> which is the primary key value of the entity and returns the entity
        /// if found otherwise <see langword="null"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="id">The primary key value of the entity.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <paramref name="id"/> which is the primary key value of the entity and returns the entity
        /// if found otherwise <see langword="null"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="id">The primary key value of the entity.</param>
        /// <param name="asNoTracking">A <see cref="bool"/> value which determines whether the return entity will be tracked by
        /// EF Core context or not. Defualt value is false i.e trackig is enabled by default.
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        Task<TEntity> GetByIdAsync(object id, bool asNoTracking, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <paramref name="id"/> which is the primary key value of the entity and returns the entity
        /// if found otherwise <see langword="null"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="id">The primary key value of the entity.</param>
        /// <param name="includes">The navigation properties to be loaded.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        Task<TEntity> GetByIdAsync(object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <paramref name="id"/> which is the primary key value of the entity and returns the entity
        /// if found otherwise <see langword="null"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="id">The primary key value of the entity.</param>
        /// <param name="includes">The navigation properties to be loaded.</param>
        /// <param name="asNoTracking">A <see cref="bool"/> value which determines whether the return entity will be tracked by
        /// EF Core context or not. Defualt value is false i.e trackig is enabled by default.
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        Task<TEntity> GetByIdAsync(object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes, bool asNoTracking, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <paramref name="id"/> which is the primary key value of the entity and returns the specified projected entity
        /// if found otherwise null.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProjectedType">The projected type.</typeparam>
        /// <param name="id">The primary key value of the entity.</param>
        /// <param name="selectExpression">The <see cref="System.Linq"/> select query.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task"/> of <typeparamref name="TResult"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="selectExpression"/> is <see langword="null"/>.</exception>
        Task<TResult> GetByIdAsync<TResult>(object id, Expression<Func<TEntity, TResult>> selectExpression, CancellationToken cancellationToken = default) where TResult : class;

        #region 查询单条数据

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> as parameter and returns <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="condition">The conditon on which entity will be returned.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <typeparamref name="TEntity"/>.</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> as parameter and returns <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="condition">The conditon on which entity will be returned.</param>
        /// <param name="asNoTracking">A <see cref="bool"/> value which determines whether the return entity will be tracked by
        /// EF Core context or not. Defualt value is false i.e trackig is enabled by default.
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <typeparamref name="TEntity"/>.</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, bool asNoTracking, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> as parameter and returns <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="condition">The conditon on which entity will be returned.</param>
        /// <param name="includes">Navigation properties to be loaded.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <typeparamref name="TEntity"/>.</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> as parameter and returns <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="condition">The conditon on which entity will be returned.</param>
        /// <param name="includes">Navigation properties to be loaded.</param>
        /// <param name="asNoTracking">A <see cref="bool"/> value which determines whether the return entity will be tracked by
        /// EF Core context or not. Defualt value is false i.e trackig is enabled by default.
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <typeparamref name="TEntity"/>.</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes, bool asNoTracking, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> as parameter and returns <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TResult">The projected type.</typeparam>
        /// <param name="condition">The conditon on which entity will be returned.</param>
        /// <param name="selectExpression">The <see cref="System.Linq"/> select query.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Retuns <typeparamref name="TProjectedType"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="selectExpression"/> is <see langword="null"/>.</exception>
        Task<TResult> GetAsync<TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> selectExpression, CancellationToken cancellationToken = default) where TResult : class;

        /// <summary>
        /// This method takes an <see cref="object"/> of <see cref="ISpecification{T}"/> as parameter and returns <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="specification">A <see cref="ISpecification{TEntity}"/> object which contains all the conditions and criteria
        /// on which data will be returned.
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        Task<TEntity> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        Task<TResult> GetAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default);

        #endregion 查询单条数据

        #region 判断数据是否存在

        /// <summary>
        /// This method takes a predicate based on which existence of the entity will be determined
        /// and returns <see cref="Task"/> of <see cref="bool"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="condition">The condition based on which the existence will checked.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="bool"/>.</returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        #endregion 判断数据是否存在

        #region 查询总数

        /// <summary>
        /// This method takes one or more <em>predicates</em> and returns the count in <see cref="int"/> type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="condition">The condition based on which count will be done.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task"/> of <see cref="int"/>.</returns>
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

        Task<int> GetCountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes one or more <em>predicates</em> and returns the count in <see cref="long"/> type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="condition">The condition based on which count will be done.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Retuns <see cref="Task"/> of <see cref="long"/>.</returns>
        Task<long> GetLongCountAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

        Task<long> GetLongCountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        #endregion 查询总数

        #region SQL 原生查询

        /// <summary>
        /// This method takes <paramref name="sql"/> string as parameter and returns the result of the provided sql.
        /// </summary>
        /// <typeparam name="T">The <see langword="type"/> to which the result will be mapped.</typeparam>
        /// <param name="sql">The sql query string.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="sql"/> is <see langword="null"/>.</exception>
        Task<List<T>> GetFromRawSqlAsync<T>(string sql, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <paramref name="sql"/> string and the value of <paramref name="parameter"/> mentioned in the sql query as parameters
        /// and returns the result of the provided sql.
        /// </summary>
        /// <typeparam name="T">The <see langword="type"/> to which the result will be mapped.</typeparam>
        /// <param name="sql">The sql query string.</param>
        /// <param name="parameter">The value of the paramter mention in the sql query.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="sql"/> is <see langword="null"/>.</exception>
        Task<List<T>> GetFromRawSqlAsync<T>(string sql, object parameter, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <paramref name="sql"/> string and values of the <paramref name="parameters"/> mentioned in the sql query as parameters
        /// and returns the result of the provided sql.
        /// </summary>
        /// <typeparam name="T">The <see langword="type"/> to which the result will be mapped.</typeparam>
        /// <param name="sql">The sql query string.</param>
        /// <param name="parameters">The values of the parameters mentioned in the sql query.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="sql"/> is <see langword="null"/>.</exception>
        Task<List<T>> GetFromRawSqlAsync<T>(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default);
        #endregion SQL 原生查询
    }
}
