using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LAP.Common;
using LAP.EntityFrameworkCore.ViewModel;

namespace LAP.EntityFrameworkCore.Application
{
    /// <summary>
    /// 统计日志服务
    /// </summary>
    public class StatisticLogService
    {
        private static readonly DapperHelper DapperHelper = new();

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public async Task<StatisticLogDto> Find(string id)
        {
            const string sql = @"SELECT t1.id, t1.module_code, t2.`name` AS 'module_name', t1.request_page, t1.action , t1.request_url, t1.message, t1.request_time, 
                                        t1.created_time FROM statistic_logs t1, modules t2 
                                 WHERE t1.module_code = t2.`code` AND t1.id=@id;";
            return await DapperHelper.QueryFirstAsync<StatisticLogDto>(sql, new { id });
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">分页下标</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="searchKey">查询条件</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public async Task<PagedList<StatisticLogDto>> PageQuery(int pageIndex, int pageSize, string searchKey, int moduleCode, string startDate, string endDate)
        {
            --pageIndex;
            var pagedList = new PagedList<StatisticLogDto>();

            var parameters = new DynamicParameters();
            parameters.Add("@pageIndex", pageIndex * pageSize);
            parameters.Add("@pageSize", pageSize);

            var sql = @"SELECT t1.id, t1.module_code, t2.`name` AS 'module_name', t1.request_page, t1.action , t1.request_url, t1.message, t1.request_time, 
                               t1.created_time FROM statistic_logs t1, modules t2 
                        WHERE t1.module_code = t2.`code` ";

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                sql += " AND t1.request_page LIKE CONCAT('%',@searchKey,'%')";
                parameters.Add("@searchKey", searchKey);
            }
            if (moduleCode > 0)
            {
                sql += " AND t1.module_code=@module_code";
                parameters.Add("@module_code", moduleCode);
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

            pagedList.total = (await DapperHelper.QueryAsync<StatisticLogDto>(sql, parameters)).Count();

            sql += " ORDER BY t1.created_time DESC LIMIT @pageIndex,@pageSize";

            pagedList.dataList = await DapperHelper.QueryAsync<StatisticLogDto>(sql, parameters);
            return pagedList;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input">输入实体</param>
        /// <returns></returns>
        public async Task<bool> Inster(StatisticLogInputDto input)
        {
            const string sql = @"INSERT INTO `statistic_logs` (`id`,`module_code`, `request_page`, `action`, `request_url`, `message`, `request_time`, `created_time` )
                                 VALUES (@id,@module_code, @request_page, @action, @request_url, @message, @request_time, @created_time);";
            var param = new
            {
                id = Tools.NewGuid,
                input.module_code,
                input.request_page,
                input.action,
                input.request_url,
                input.message,
                input.request_time,
                created_time = DateTime.Now
            };
            return await DapperHelper.ExecuteAsync(sql, param);
        }
    }
}
