using Dapper;
using Newtonsoft.Json;
using sun.Core.Domains;
using sun.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.Marshalling.IIUnknownCacheStrategy;

namespace sun.Core.Extensions
{
    /// <summary>
    /// 数据采集扩展
    /// </summary>
    public static class CollectExtensions
    {

        public static dynamic GetRedisKeys()
        {
            Dictionary<string, object> redisKeys = new Dictionary<string, object>();
            return false;
        }

        /// <summary>
        /// 将DataTable首行转换为ExpandoObject对象
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static dynamic ConvertExpandoObject(this DataTable dataTable)
        {
            var expandoObject = new ExpandoObject() as IDictionary<string, object>;

            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                return null;
            }

            var myRow = dataTable.Rows[0];
            foreach (DataColumn col in myRow.Table.Columns)
            {
                if (myRow[col] == DBNull.Value)
                {
                    expandoObject[col.ColumnName] = "";
                }
                else
                {
                    expandoObject[col.ColumnName] = myRow[col];
                }
            }

            return expandoObject;
        }


        /// <summary>
        /// 将指标查询的参数进行转换，使参数名不以@开头的，都变成@开头的参数
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Dictionary<string, object> RebuilderMetaDataQueryParemeter(this Dictionary<string, object> parameters)
        {
            Dictionary<string, object> sqlParameters = new Dictionary<string, object>();
            foreach (var key in parameters.Keys)
            {
                if (!key.StartsWith('@'))
                {
                    sqlParameters["@" + key] = parameters[key];
                }
                else
                {
                    sqlParameters[key] = parameters[key];
                }
            }
            return sqlParameters;
        }

        /// <summary>
        /// 检查数据是否是新数据，如果是新数据则返回true，否则返回false
        /// </summary>
        /// <param name="form"></param>
        /// <param name="formData"></param>
        /// <param name="dbConnection"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        public static async Task<bool> CheckDataIsNew(this CollectFormMetaData form, Dictionary<string, object> formData, DbTransaction dbTransaction)
        {
            string id = string.Empty;
            if (formData.ContainsKey("id"))
            {
                //如果数据包中含有id，则取出id
                id = formData["id"].ToString();
                if (string.IsNullOrWhiteSpace(id))
                {
                    //如果id为空，则新建一个id值,并返回新记录
                    id = Guid.NewGuid().ToString();
                    return true;
                }
            }
            else
            {
                //数据数据包中不含有id，则新建一个id值，插入数据包，并返回新记录
                id = Guid.NewGuid().ToString();
                formData.Add("id", id);
                return true;
            }

            //如果有id值，则在数据库中查找是否有记录，如果有记录则返回false，否则返回true
            List<string> writeTables = JsonConvert.DeserializeObject<List<string>>(form.WriteTables);

            bool isNew = true;

            var tablename = writeTables[0];
            var exists = await dbTransaction.Connection.ExecuteScalarAsync<string>($"select id from {tablename} where id = '{id}' ", transaction: dbTransaction);
            if (!string.IsNullOrEmpty(exists))
            {
                isNew = false;
            }
            return isNew;
        }
        /// <summary>
        /// 检查通过录入模型获取的数据中是否有id，如果没有则新建一个id，如果没有记录则建一条
        /// </summary>
        /// <param name="dt"></param>
        public static void CheckIdColumnAndAddRow(this DataTable dt)
        {
            if (dt == null)
            {
                return;
            }
            if (!dt.Columns.Contains("id"))
            {
                dt.Columns.Add("id");
                if (dt.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                }
                dt.Rows[0]["id"] = Guid.NewGuid().ToString();
            }
        }
    }
}
