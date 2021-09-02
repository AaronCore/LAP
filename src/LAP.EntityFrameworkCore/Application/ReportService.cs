using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using LAP.Common;
using LAP.EntityFrameworkCore.Model;

namespace LAP.EntityFrameworkCore.Application
{
    public class ReportService
    {
        private static readonly DapperHelper DapperHelper = new();

        public async Task<List<TableTreeModel>> TableData()
        {
            using var conn = DapperHelper.Connection();
            {
                using var transaction = conn.BeginTransaction();

                try
                {
                    var tableTreeList = new List<TableTreeModel>();

                    var tableList = await conn.QueryAsync<TableSchemaModel>("SELECT TABLE_SCHEMA,TABLE_NAME,TABLE_COMMENT,TABLE_ROWS,CREATE_TIME,UPDATE_TIME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='lap' ORDER BY TABLE_NAME;");
                    foreach (var table in tableList)
                    {
                        var tableTree = new TableTreeModel()
                        {
                            id = table.TABLE_NAME,
                            name = $"{table.TABLE_NAME}({table.TABLE_COMMENT})",
                        };

                        var tableColumnQuery = await conn.QueryAsync<TableColumnModel>("SELECT COLUMN_NAME,COLUMN_COMMENT FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'lap' AND TABLE_NAME =@TABLE_NAME;", new { TABLE_NAME = table.TABLE_NAME });
                        var tableColumnList = tableColumnQuery.Select(p => new TreeModel()
                        {
                            id = p.COLUMN_NAME,
                            name = $"{p.COLUMN_NAME}({p.COLUMN_COMMENT})"
                        });
                        tableTree.children = tableColumnList.ToList();

                        tableTreeList.Add(tableTree);
                    }

                    return tableTreeList;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return null;
                }
                finally
                {
                    transaction.Dispose();
                    conn.Dispose();
                }
            }
        }

        public async Task<TableModel> SqlQuery(string sql)
        {
            var tableModel = new TableModel();
            var table = await Query(sql);

            // 填充列
            foreach (DataColumn col in table.Columns)
            {
                tableModel.Columns.Add(col.ColumnName);
            }

            // 填充行
            foreach (DataRow row in table.Rows)
            {
                var list = row.ItemArray.Select(item => item?.ToString()).ToList();
                tableModel.Rows.Add(list);
            }

            return tableModel;
        }

        public async Task<DataTable> Query(string sql)
        {
            var table = await DapperHelper.QueryAsync(sql);
            return table;
        }
    }
}
