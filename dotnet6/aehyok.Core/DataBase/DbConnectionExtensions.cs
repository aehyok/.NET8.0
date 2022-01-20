using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace aehyok.Core.DataBase
{
    /// <summary>
    /// DbConnection扩展
    /// </summary>
    public static class DbConnectionExtensions
    {
        /// <summary>
        /// 操作数据库(推荐用于查询)，如需操作事务(Transaction)，建议使用UsingTransacion。
        /// 若要操作事务(Transaction)，务必在Func中有Transacion.Commit()。
        /// </summary>
        /// <typeparam name="TDbConn">表示的开放连接到数据源，并由访问关系数据库的.NET Framework 数据提供程序实现</typeparam>
        /// <typeparam name="TResultValue">操作数据库后返回值</typeparam>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="func">操作数据库的Func</param>
        /// <returns></returns>
        public static TResultValue Using<TDbConn, TResultValue>(this TDbConn dbConnection, Func<TDbConn, TResultValue> func)
            where TDbConn : IDbConnection
        {
            TResultValue result = default(TResultValue);
            using (dbConnection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                result = func(dbConnection);
                dbConnection.Close();
            }
            return result;
        }

        /// <summary>
        /// 操作数据库(推荐用于查询)，如需操作事务(Transaction)，建议使用UsingTransacion。
        /// 若要操作事务(Transaction)，务必在Action中有Transacion.Commit()。
        /// </summary>
        /// <typeparam name="T">表示的开放连接到数据源，并由访问关系数据库的.NET Framework 数据提供程序实现</typeparam>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="work">操作数据库的Action</param>
        public static void Using<T>(this T dbConnection, Action<T> work)
            where T : IDbConnection
        {
            using (dbConnection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                work(dbConnection);
                dbConnection.Close();
            }
        }

        /// <summary>
        /// 操作数据库,内置事务(Transaction),事务会自动提交或回滚,可用于数据库的增删改。(对于SqlServer数据库不适用）
        /// </summary>
        /// <typeparam name="TDbConn">表示的开放连接到数据源，并由访问关系数据库的.NET Framework 数据提供程序实现</typeparam>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="func">数据库增删改的Func</param>
        /// <returns></returns>
        public static TResult UsingTransacion<TDbConn, TResult>(this TDbConn dbConnection, Func<TDbConn, TResult> func)
            where TDbConn : IDbConnection
        {
            TResult result = default(TResult);
            using (dbConnection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                using (IDbTransaction transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        result = func(dbConnection);
                        transaction.Commit();
                    }
                    catch (Exception tranExc)
                    {
                        transaction.Rollback();
                        //throw tranExc;
                    }
                }
                dbConnection.Close();
            }
            return result;
        }

        /// <summary>
        /// 操作数据库,内置事务(Transaction),事务不会自动提交或回滚,可用于数据库的增删改,需自行决定事务的提交或回滚。
        /// </summary>
        /// <typeparam name="TDbConn">表示的开放连接到数据源，并由访问关系数据库的.NET Framework 数据提供程序实现</typeparam>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="func">数据库增删改的Func</param>
        /// <returns></returns>
        public static TResult UsingTransacion<TDbConn, TResult>(this TDbConn dbConnection, Func<TDbConn, IDbTransaction, TResult> func)
            where TDbConn : IDbConnection
        {
            TResult result = default(TResult);
            using (dbConnection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                using (IDbTransaction transaction = dbConnection.BeginTransaction())
                {
                    result = func(dbConnection, transaction);
                }
                dbConnection.Close();
            }
            return result;
        }

        /// <summary>
        /// 操作数据库,内置事务(Transaction),事务会自动提交或回滚,可用于数据库的增删改。(对于SqlServer数据库不适用）
        /// </summary>
        /// <typeparam name="T">表示的开放连接到数据源，并由访问关系数据库的.NET Framework 数据提供程序实现</typeparam>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="work">数据库增删改的Action</param>
        public static void UsingTransacion<T>(this T dbConnection, Action<T> work)
            where T : IDbConnection
        {
            using (dbConnection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                using (IDbTransaction transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        work(dbConnection);
                        transaction.Commit();
                    }
                    catch (Exception tranExc)
                    {
                        transaction.Rollback();
                        //throw tranExc;
                    }
                }
                dbConnection.Close();
            }
        }

        /// <summary>
        /// 操作数据库,内置事务(Transaction),事务不会自动提交或回滚,可用于数据库的增删改,需自行决定事务的提交或回滚。
        /// </summary>
        /// <typeparam name="T">表示的开放连接到数据源，并由访问关系数据库的.NET Framework 数据提供程序实现</typeparam>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="work">数据库增删改的Action</param>
        public static void UsingTransacion<T>(this T dbConnection, Action<T, IDbTransaction> work)
            where T : IDbConnection
        {
            using (dbConnection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                using (IDbTransaction transaction = dbConnection.BeginTransaction())
                {
                    work(dbConnection, transaction);
                }
                dbConnection.Close();
            }
        }
    }
}
