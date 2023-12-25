using aehyok.EntityFramework.Entities;
using aehyok.EntityFramework.Repository.AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.EntityFramework.Repository
{
    public interface IServiceBase<TEntity> : IServiceBase<TEntity, long> where TEntity : class, IEntity<long>
    {
        /// <summary>
        /// 根据主键删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ErrorCodeException"></exception>
        Task<int> DeleteAsync(long id, CancellationToken cancellationToken = default);
    }

    public interface IServiceBase<TEntity, TKey> : IAutoMapperRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// 通过指定条件判断，更新或插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task InsertOrUpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> condition);

        /// <summary>
        /// 通过 Id 判断，更新或插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertOrUpdateAsync(TEntity entity);

        /// <summary>
        /// 批量删除，返回受影响的行数
        /// 该方法是硬删除，请谨慎使用。该方法执行 SQL 不会在 Log 中输出，请谨慎使用
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>受影响的行数</returns>
        Task<int> BatchDeleteAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量软删除，返回删除的数据行数
        /// 该方法是根据条件批量删除，执行的 SQL 不会在 Log 中输出，请谨慎使用
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>受影响的行数</returns>
        Task<int> BatchSoftDeleteAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询更新
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="updateExpression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> UpdateFromQueryAsync(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TEntity>> updateExpression, CancellationToken cancellationToken = default);

        DbContext GetDbContext { get; }
    }
}
