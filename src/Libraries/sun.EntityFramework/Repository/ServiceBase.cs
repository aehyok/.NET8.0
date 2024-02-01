using sun.EntityFrameworkCore.Entities;
using sun.EntityFrameworkCore.Repository.AutoMapper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace sun.EntityFrameworkCore.Repository
{
    public partial class ServiceBase<TEntity, TKey> : AutoMapperRepository<TEntity, TKey>, IServiceBase<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public DbContext GetDbContext => this.DbContext;

        public ServiceBase(DbContext dbContext, IMapper mapper)
        : base(dbContext, mapper)
        {
        }

        public virtual async Task InsertOrUpdateAsync(TEntity entity)
        {
            var exists = await this.GetByIdAsync(entity.Id, true);
            if (exists is null)
            {
                await this.InsertAsync(entity);
            }
            else
            {
                await this.UpdateAsync(entity);
            }

            this.ResetContextState();
        }

        public virtual async Task InsertOrUpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> condition)
        {
            var exists = await this.GetAsync(condition);
            if (exists is null)
            {
                await this.InsertAsync(entity);
            }
            else
            {
                entity.Id = exists.Id;
                await this.UpdateAsync(entity);
            }

            this.ResetContextState();
        }

        /// <summary>
        /// 批量删除，返回删除的数据行数
        /// 该方法是硬删除，请谨慎使用。该方法执行 SQL 不会在 Log 中输出，请谨慎使用
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> BatchDeleteAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            return this.Entities.Where(condition).DeleteFromQueryAsync(cancellationToken);
        }

        /// <summary>
        /// 批量软删除，返回删除的数据行数
        /// 该方法是根据条件批量删除，执行的 SQL 不会在 Log 中输出，请谨慎使用
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<int> BatchSoftDeleteAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            var result = 0;

            if (typeof(TEntity).IsAssignableTo(typeof(ISoftDelete)))
            {
                var data = new ExpandoObject();
                data.TryAdd(nameof(ISoftDelete.IsDeleted), true);

                result = await this.Entities.Where(condition).UpdateFromQueryAsync(data);
            }

            return result;
        }

        /// <summary>
        /// 查询更新
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="updateExpression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> UpdateFromQueryAsync(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TEntity>> updateExpression, CancellationToken cancellationToken = default)
        {
            return this.GetQueryable().Where(condition).UpdateFromQueryAsync(updateExpression, cancellationToken);
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> HeadDeleteAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            return this.Entities.Where(condition).DeleteFromQueryAsync(cancellationToken);
        }

        public virtual DbConnection GetConnection()
        {
            return this.DbContext.Database.GetDbConnection();
        }
    }

    public partial class ServiceBase<TEntity> : ServiceBase<TEntity, long>, IServiceBase<TEntity> where TEntity : class, IEntity<long>
    {
        public ServiceBase(DbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        /// <summary>
        /// 根据主键删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ErrorCodeException"></exception>
        public virtual async Task<int> DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            if (typeof(TEntity).IsAssignableTo(typeof(ISoftDelete)))
            {
                var data = new ExpandoObject();
                data.TryAdd(nameof(ISoftDelete.IsDeleted), true);

                return await this.Entities.Where(a => a.Id == id).UpdateFromQueryAsync(data);
            }

            var entity = await this.GetByIdAsync(id);
            if (entity is null)
            {
                return 0;
            }

            return await this.DeleteAsync(entity, cancellationToken);
        }

        public static void FillTableByReader(DataTable data, IDataReader rdr)
        {
            int i;
            DataRow _newrow;
            int _fcount = rdr.FieldCount;

            DataTable schemaTable = rdr.GetSchemaTable();
            List<int> NumberColumnList = new List<int>();

            int index = 0;
            foreach (DataRow myRow in schemaTable.Rows)
            {
                data.Columns.Add(myRow["ColumnName"].ToString(), (Type)myRow["DataType"]);
                if ((Type)myRow["DataType"] == typeof(decimal))
                {
                    NumberColumnList.Add(index);
                }
                index++;
            }

            while (rdr.Read())
            {
                _newrow = data.NewRow();
                for (i = 0; i < _fcount; i++)
                {
                    if (NumberColumnList.Contains(i) && !rdr.IsDBNull(i))
                    {
                        _newrow[i] = rdr.GetDecimal(i);
                    }
                    else
                    {
                        if (rdr[i].GetType().Name == "DateTime" || rdr[i].GetType().Name == "MySqlDateTime")
                        {
                            if (rdr[i] != null && (rdr[i].ToString() == "0001/1/1 0:00:00" || rdr[i].ToString() == "0000-00-00 00:00:00" || rdr[i].ToString() == "1/1/0001 12:00:00 AM"))
                            {
                                var ss = rdr[i].ToString();
                                _newrow[i] = DBNull.Value;
                            }
                            else
                            {
                                _newrow[i] = rdr[i];
                            }
                        }
                        else if (rdr[i].GetType().Name == "String")
                        {
                            if (rdr[i] is DBNull)
                            {
                                _newrow[i] = "";
                            }
                            else
                            {
                                _newrow[i] = rdr[i];
                            }
                        }
                        else
                        {
                            _newrow[i] = rdr[i];
                        }

                        //if (rdr[i] != null && (rdr[i].ToString() == "0001/1/1 0:00:00" || rdr[i].ToString() == "0000-00-00 00:00:00" || rdr[i].ToString() == "1/1/0001 12:00:00 AM"))
                        //{
                        //    var ss = rdr[i].ToString();
                        //    _newrow[i] = DBNull.Value;
                        //}
                        //else
                        //{
                        //    _newrow[i] = rdr[i];

                        //}
                    }
                }
                data.Rows.Add(_newrow);
            }
            data.AcceptChanges();
        }
    }
}
