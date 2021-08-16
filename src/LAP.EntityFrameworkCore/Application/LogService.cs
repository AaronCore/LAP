using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LAP.Common;
using LAP.EntityFrameworkCore.ViewModel;

namespace LAP.EntityFrameworkCore.Application
{
    /// <summary>
    /// 日志服务
    /// </summary>
    public class LogService
    {
        private static readonly DapperHelper DapperHelper = new();

        /// <summary>
        /// 添加Log
        /// </summary>
        /// <param name="input">Log模型</param>
        /// <returns></returns>
        public async Task<bool> Inster(LogInputDto input)
        {
            const string sql = @"INSERT INTO `logs` (`module_code`, `level`, `request_path`, `request_url`, `request_form`, `method`, `exception`, `message`, `ip_address`, `remark`, `log_create_time`, `created_time` )
                                 VALUES (@module_code, @level, @request_path, @request_url, @request_form , @method, @exception, @message, @ip_address, @remark , @log_create_time, @created_time);";
            var param = new
            {
                input.module_code,
                input.level,
                input.request_path,
                input.request_url,
                input.request_form,
                input.method,
                input.exception,
                input.message,
                input.ip_address,
                input.remark,
                input.log_create_time,
                created_time = DateTime.Now
            };
            return await DapperHelper.ExecuteAsync(sql, param);
        }

        /// <summary>
        /// 获取Log
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public async Task<LogDto> Find(int id)
        {
            const string sql = @"SELECT t1.id, t1.module_code, t2.`name` AS 'module_name', t1.`level`,t1.request_path, t1.request_url, t1.request_form, t1.method, t1.exception, 
                                        t1.message, t1.ip_address, t1.remark, t1.log_create_time, t1.created_time FROM `logs` AS t1, modules AS t2 
                                 WHERE 1=1 AND t1.module_code = t2.`code` AND t1.id=@id;";
            return await DapperHelper.QueryFirstAsync<LogDto>(sql, new { id });
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">分页下标</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="searchKey">查询条件</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="logLevel">日志等级</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public async Task<PagedList<LogDto>> PageQuery(int pageIndex, int pageSize, string searchKey, int moduleCode, int logLevel, string startDate, string endDate)
        {
            --pageIndex;
            var pagedList = new PagedList<LogDto>();

            var parameters = new DynamicParameters();
            parameters.Add("@pageIndex", pageIndex * pageSize);
            parameters.Add("@pageSize", pageSize);

            var sql = @"SELECT t1.id, t1.module_code, t2.`name` AS 'module_name', t1.`level`,t1.request_path, t1.request_url, t1.request_form, t1.method, t1.exception, 
                               t1.message, t1.ip_address, t1.remark, t1.log_create_time, t1.created_time FROM `logs` AS t1, modules AS t2 
                        WHERE 1=1 AND t1.module_code = t2.`code` ";

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                sql += " AND t1.request_path LIKE CONCAT('%',@searchKey,'%')";
                parameters.Add("@searchKey", searchKey);
            }
            if (moduleCode > 0)
            {
                sql += " AND t1.module_code=@module_code";
                parameters.Add("@module_code", moduleCode);
            }
            if (logLevel > 0)
            {
                sql += " AND t1.level=@level";
                parameters.Add("@level", logLevel);
            }
            if (!string.IsNullOrWhiteSpace(startDate))
            {
                sql += " AND DATE(t1.created_time)>=@startDate";
                parameters.Add("@startDate", startDate);
            }
            if (!string.IsNullOrWhiteSpace(endDate))
            {
                sql += " AND DATE(t1.created_time)<=@endDate";
                parameters.Add("@endDate", endDate);
            }

            pagedList.total = (await DapperHelper.QueryAsync<LogDto>(sql, parameters)).Count();

            sql += " ORDER BY t1.created_time DESC LIMIT @pageIndex,@pageSize";

            pagedList.dataList = await DapperHelper.QueryAsync<LogDto>(sql, parameters);
            return pagedList;
        }

    }
}
