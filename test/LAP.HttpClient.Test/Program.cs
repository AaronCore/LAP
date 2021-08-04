using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LAP.HttpClient.Enum;
using LAP.HttpClient.Model;

namespace LAP.HttpClient.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            for (int i = 1; i <= 500; i++)
            {
                Thread.Sleep(1000);

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{DateTime.Now}  第{i}次执行...");

                // 种子数据
                var logData = SeedData.CreateLogData();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(logData.ToJson());

                // add log
                var logModel = new AddLogModel()
                {
                    module_code = logData.module_code,
                    level = logData.level,
                    request_path = logData.path,
                    request_url = logData.url,
                    method = logData.method.ToString(),
                    exception = logData.exception,
                    message = logData.message,
                    ip_address = logData.ip,
                    log_create_time = logData.date
                };
                await Manager.Log(logModel.ToJson());

                // add statistic log
                var statisticLogModel = new AddStatisticLogModel()
                {
                    module_code = logData.module_code,
                    request_page = logData.path,
                    action = logData.action,
                    request_url = logData.url,
                    request_time = logData.date
                };
                await Manager.StatisticLog(statisticLogModel.ToJson());
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("end...");
            Console.ReadKey();
        }

        /// <summary>
        /// Add Log Test
        /// </summary>
        /// <returns></returns>
        public static async Task Add_Log_Test()
        {
            var logModel = new AddLogModel()
            {
                module_code = 102,
                level = LogLevel.Info,
                request_path = "/Home/Index",
                request_url = "/Home/Index",
                method = "GET",
                message = $"时间：{DateTime.Now}",
                ip_address = "127.0.0.1",
                log_create_time = DateTime.Now
            };
            await Manager.Log(logModel.ToJson());
        }

        /// <summary>
        /// Add StatisticLog Test
        /// </summary>
        /// <returns></returns>
        public static async Task Add_StatisticLog_Test()
        {
            var statisticLogModel = new AddStatisticLogModel()
            {
                module_code = 102,
                request_page = "Index页面",
                action = StatisticAction.页面访问,
                request_url = "http://10.10.1.22:3200/Home/Index",
                request_time = DateTime.Now
            };
            await Manager.StatisticLog(statisticLogModel.ToJson());
        }
    }
}
