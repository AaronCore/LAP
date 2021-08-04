using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using LAP.HttpClient;
using LAP.HttpClient.Enum;
using LAP.HttpClient.Model;
using Newtonsoft.Json;

namespace WebMvc.Example
{
    public class LapLogger
    {
        private const int MODULE_CODE = 102;

        /// <summary>
        /// 添加Log
        /// </summary>
        /// <param name="context">HttpContext上下文</param>
        /// <param name="level">日志等级</param>
        /// <param name="message">普通信息</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public static async Task AddLog(HttpContextBase context, LogLevel level, string message = null, string remark = null)
        {
            var dic = new Dictionary<string, object>();
            if (context.Request.Form.Count > 0)
            {
                foreach (string key in context.Request.Form.Keys)
                {
                    var v = context.Request.Form[key][0];
                    dic.Add(key, v);
                }
            }
            var logModel = new AddLogModel()
            {
                module_code = MODULE_CODE,
                level = level,
                request_path = context.Request.Path,
                request_url = context.Request.Url?.AbsoluteUri,
                request_form = JsonConvert.SerializeObject(dic),
                method = context.Request.HttpMethod,
                exception = level == LogLevel.Error ? context.Error.ToString() : null,
                message = message,
                ip_address = context.Request.UserHostAddress,
                remark = remark,
                log_create_time = DateTime.Now
            };
            await Manager.Log(JsonConvert.SerializeObject(logModel));
        }

        /// <summary>
        /// 统计日志
        /// </summary>
        /// <param name="context">HttpContext上下文</param>
        /// <param name="action">执行动作,例如：登录、登出...</param>
        /// <param name="message">说明信息</param>
        /// <returns></returns>
        public static async Task AddStatisticLog(HttpContextBase context, StatisticAction action, string message = null)
        {
            var statisticLogModel = new AddStatisticLogModel()
            {
                module_code = MODULE_CODE,
                request_page = context.Request.Path,
                action = action,
                request_url = context.Request.Url?.AbsoluteUri,
                request_time = DateTime.Now
            };
            await Manager.StatisticLog(JsonConvert.SerializeObject(statisticLogModel));
        }
    }
}