using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Dapper;
using LAP.Common;

namespace LAP.EntityFrameworkCore
{
    /// <summary>
    /// Dapper帮助类
    /// </summary>
    public class DapperHelper
    {
        private static readonly ConfigHelper ConfigHelper = new ConfigHelper();
        private static readonly string ConnectionString = ConfigHelper.GetValue<string>("MySQLConnection");

        public DapperHelper()
        {

        }

        public IDbConnection Connection()
        {
            var connection = new MySqlConnection(ConnectionString);
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            if (connection.State == ConnectionState.Broken)
                connection.Close();
            return connection;
        }

        #region Execute

        public virtual bool Execute(string sql, object param = null)
        {
            using (var conn = Connection())
            {
                return conn.Execute(sql, param) > 0;
            }
        }
        public virtual async Task<bool> ExecuteAsync(string sql, object param = null)
        {
            using (var conn = Connection())
            {
                return await conn.ExecuteAsync(sql, param) > 0;
            }
        }

        #endregion

        #region ExecuteScalar

        public virtual T ExecuteScalar<T>(string sql, object param = null)
        {
            using (var conn = Connection())
            {
                return conn.ExecuteScalar<T>(sql, param);
            }
        }
        public virtual async Task<T> ExecuteScalarAsync<T>(string sql, object param = null)
        {
            using (var conn = Connection())
            {
                return await conn.ExecuteScalarAsync<T>(sql, param);
            }
        }

        #endregion

        #region Query

        public virtual IEnumerable<T> Query<T>(string sql, object param = null)
        {
            using (var conn = Connection())
            {
                return conn.Query<T>(sql, param);
            }
        }
        public virtual async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
        {
            using (var conn = Connection())
            {
                return await conn.QueryAsync<T>(sql, param);
            }
        }

        #endregion

        #region QueryFirst

        public virtual T QueryFirst<T>(string sql, object param = null)
        {
            using (var conn = Connection())
            {
                return conn.QueryFirstOrDefault<T>(sql, param);
            }
        }
        public virtual async Task<T> QueryFirstAsync<T>(string sql, object param = null)
        {
            using (var conn = Connection())
            {
                return await conn.QueryFirstOrDefaultAsync<T>(sql, param);
            }
        }

        #endregion
    }
}
