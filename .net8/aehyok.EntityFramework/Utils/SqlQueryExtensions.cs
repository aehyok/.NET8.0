﻿using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.EntityFramework.Utils
{
    /// <summary>
    /// internal只能在同一个程序集中访问
    /// </summary>
    internal static class SqlQueryExtensions
    {
        public static async Task<List<T>> GetFromQueryAsync<T>(this DbContext dbContext, string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            using DbCommand command = dbContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = sql;

            if (parameters != null)
            {
                int index = 0;
                foreach (object item in parameters)
                {
                    DbParameter dbParameter = command.CreateParameter();
                    dbParameter.ParameterName = "@p" + index;
                    dbParameter.Value = item;
                    command.Parameters.Add(dbParameter);
                    index++;
                }
            }

            await dbContext.Database.OpenConnectionAsync(cancellationToken);

            using DbDataReader result = await command.ExecuteReaderAsync(cancellationToken);

            List<T> list = new List<T>();
            T obj = default;
            while (result.Read())
            {
                if (!(typeof(T).IsPrimitive || typeof(T).Equals(typeof(string))))
                {
                    obj = Activator.CreateInstance<T>();
                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        if (!Equals(result[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, result[prop.Name], null);
                        }
                    }

                    list.Add(obj);
                }
                else
                {
                    obj = (T)Convert.ChangeType(result[0], typeof(T), CultureInfo.InvariantCulture);
                    list.Add(obj);
                }
            }

            return list;
        }

        private static async Task<List<T>> GetFromQueryAsync2<T>(this DbContext dbContext, string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default)
        {
            RelationalDataReader relationalDataReader = await dbContext.ExecuteSqlQueryAsync(sql, parameters, cancellationToken);
            using DbDataReader dr = relationalDataReader.DbDataReader;

            List<T> list = new List<T>();
            T t = default;
            while (dr.Read())
            {
                if (!(typeof(T).IsPrimitive || typeof(T).Equals(typeof(string))))
                {
                    PropertyInfo[] props = typeof(T).GetProperties();
                    IEnumerable<string> actualNames = dr.GetColumnSchema().Select(o => o.ColumnName);
                    for (int i = 0; i < props.Length; ++i)
                    {
                        PropertyInfo pi = props[i];

                        if (!pi.CanWrite)
                        {
                            continue;
                        }

                        ColumnAttribute ca = pi.GetCustomAttribute(typeof(ColumnAttribute)) as ColumnAttribute;
                        string name = ca?.Name ?? pi.Name;

                        if (pi == null)
                        {
                            continue;
                        }

                        if (!actualNames.Contains(name))
                        {
                            continue;
                        }

                        object value = dr[name];
                        Type pt = pi.DeclaringType;
                        bool nullable = pt.GetTypeInfo().IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>);
                        if (value == DBNull.Value)
                        {
                            value = null;
                        }

                        if (value == null && pt.GetTypeInfo().IsValueType && !nullable)
                        {
                            value = Activator.CreateInstance(pt);
                        }

                        pi.SetValue(t, value);
                    }

                    list.Add(t);
                }
                else
                {
                    t = (T)Convert.ChangeType(dr[0], typeof(T), CultureInfo.InvariantCulture);
                    list.Add(t);
                }
            }

            return list;
        }

        private static async Task<RelationalDataReader> ExecuteSqlQueryAsync(this DbContext dbContext, string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            IConcurrencyDetector concurrencyDetector = dbContext.GetService<IConcurrencyDetector>();

            using (concurrencyDetector.EnterCriticalSection())
            {
                RawSqlCommand rawSqlCommand = dbContext
                    .GetService<IRawSqlCommandBuilder>()
                    .Build(sql, parameters);

                RelationalCommandParameterObject paramObject = new RelationalCommandParameterObject(
                    dbContext.GetService<IRelationalConnection>(),
                    rawSqlCommand.ParameterValues,
                    null,
                    null,
                    null);

                RelationalDataReader relationalDataReader = await rawSqlCommand
                    .RelationalCommand
                    .ExecuteReaderAsync(paramObject, cancellationToken);

                return relationalDataReader;
            }
        }
    }
}
