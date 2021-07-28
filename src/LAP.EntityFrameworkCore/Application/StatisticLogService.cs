using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAP.EntityFrameworkCore.ViewModel;

namespace LAP.EntityFrameworkCore.Application
{
    /// <summary>
    /// 统计日志服务
    /// </summary>
    public class StatisticLogService
    {
        private readonly DapperHelper _dapperHelper;
        public StatisticLogService(DapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        /// <summary>
        /// 添加StatisticLog
        /// </summary>
        /// <param name="input">Log模型</param>
        /// <returns></returns>
        public async Task<bool> InsterStatisticLog(StatisticLogInputDto input)
        {
            var sql = @"INSERT INTO `statistic_logs` (`module_code`, `request_page`, `action`, `request_url`, `message`, `request_time`, `created_time` )
                        VALUES
	                       (@module_code, @request_page, @action, @request_url, @message, @request_time, @created_time);";
            var param = new
            {
                input.module_code,
                input.request_page,
                input.action,
                input.request_url,
                input.message,
                input.request_time,
                create_time = DateTime.Now
            };
            return await _dapperHelper.ExecuteAsync(sql, param);
        }
    }
}
