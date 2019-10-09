using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace aehyok.Core.DataAccess
{
    public class MySqlDbAccessor : IDbAccossor
    {
        private readonly IConfiguration _configuration;
        private readonly string _defaultConnectionStr;
        public MySqlDbAccessor(IConfiguration configuration, string configConnectionName = "DefaultConnection")
        {
            this._configuration = configuration;
            this._defaultConnectionStr = this._configuration.GetConnectionString(configConnectionName);
        }

        public IDbConnection DbDefaultConnection => new MySqlConnection(_defaultConnectionStr);

        public IDbConnection GetConnection(string configConnectionName)
        {
            if (string.IsNullOrEmpty(configConnectionName))
            {
                throw new ArgumentNullException("configConnectionName");
            }
            string connectionStr = this._configuration.GetConnectionString(configConnectionName);
            if (string.IsNullOrEmpty(connectionStr))
            {
                throw new ArgumentException($"配置文件中未找到对应的数据库链接名称【{configConnectionName}】");
            }
            return new MySqlConnection(connectionStr);
        }
    }


}
