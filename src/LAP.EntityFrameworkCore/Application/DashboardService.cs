using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace LAP.EntityFrameworkCore.Application
{
    public class DashboardService
    {
        private static readonly DapperHelper DapperHelper = new();

        public async Task<Dictionary<string, int>> Statistics()
        {
            using var conn = DapperHelper.Connection();
            {
                using var transaction = conn.BeginTransaction();
                try
                {
                    var dic = new Dictionary<string, int>();

                    var requestNum = await conn.ExecuteScalarAsync<int>("SELECT COUNT(id) AS 'rows' FROM `statistic_logs`;");
                    dic.Add("requestNum", requestNum);

                    var logNum = await conn.ExecuteScalarAsync<int>("SELECT COUNT(id) AS 'rows' FROM `logs`;");
                    dic.Add("logNum", logNum);

                    dic.Add("monitorNum", 0);

                    var moduleNum = await conn.ExecuteScalarAsync<int>("SELECT COUNT(id) AS 'rows' FROM `modules`;");
                    dic.Add("moduleNum", moduleNum);

                    transaction.Commit();
                    return dic;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return null;
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }
    }
}
