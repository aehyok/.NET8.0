using aehyok.EntityFrameworkCore.Repository.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.EntityFrameworkCore.Repository.Base
{
    /// <summary>
    /// Contains all the repository methods.
    /// </summary>
    public interface IRepositoryBase<TEntity, TKey> : IQueryRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Begin a new database transaction.
        /// </summary>
        /// <param name="isolationLevel"><see cref="IsolationLevel"/> to be applied on this transaction. (Default to <see cref="IsolationLevel.Unspecified"/>).</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns a <see cref="IDbContextTransaction"/> instance.</returns>
        Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Unspecified, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <typeparamref name="TEntity"/>, insert it into database and returns <see cref="Task{TResult}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity to be inserted.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task"/>.</returns>
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <typeparamref name="TEntity"/>, insert it into the database and returns <see cref="Task"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entities">The entities to be inserted.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task"/>.</returns>
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <typeparamref name="TEntity"/>, send update operation to the database and returns <see cref="void"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity to be updated.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task"/>.</returns>
        Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <see cref="IEnumerable{TEntity}"/> of entities, send update operation to the database and returns <see cref="void"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entities">The entities to be updated.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task"/>.</returns>
        Task<int> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes an entity of type <typeparamref name="TEntity"/>, delete the entity from database and returns <see cref="void"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity to be deleted.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task"/>.</returns>
        Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <see cref="IEnumerable{T}"/> of entities, delete those entities from database and returns <see cref="void"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entities">The list of entities to be deleted.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task"/>.</returns>
        Task<int> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute raw sql command against the configured database asynchronously.
        /// </summary>
        /// <param name="sql">The sql string.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute raw sql command against the configured database asynchronously.
        /// </summary>
        /// <param name="sql">The sql string.</param>
        /// <param name="parameters">The paramters in the sql string.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);

        /// <summary>
        /// Execute raw sql command against the configured database asynchronously.
        /// </summary>
        /// <param name="sql">The sql string.</param>
        /// <param name="parameters">The paramters in the sql string.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        Task<int> ExecuteSqlCommandAsync(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Reset the DbContext state by removing all the tracked and attached entities.
        /// </summary>
        void ResetContextState();
    }
}
