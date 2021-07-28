using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<bool> InsterLog(LogInputDto input)
        {
            const string sql = @"INSERT INTO `logs` ( `id`, `module_code`, `level`, `request_url`, `request_body`, `method`, `exception`, `message`, `ip_address`, `remark`, `log_create_time`, `create_time` )
                                 VALUES (@module_code, @level, @request_url, @request_body , @method, @exception, @message, @ip_address, @remark , @log_create_time, @create_time);";
            var param = new
            {
                input.module_code,
                input.level,
                input.request_url,
                input.request_body,
                input.method,
                input.exception,
                input.message,
                input.ip_address,
                input.remark,
                input.log_create_time,
                create_time = DateTime.Now
            };
            return await DapperHelper.ExecuteAsync(sql, param);
        }
    }
}
