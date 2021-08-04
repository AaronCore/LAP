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
            Random r = new Random();

            for (int i = 1; i <= 500; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{DateTime.Now}  第{i}次执行...");

                var num = r.Next(10);
                Thread.Sleep(num * 1000);

                // 种子数据
                var data = SeedData.LogSeedData();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(data.ToJson());

                // add log
                var logModel = new AddLogModel()
                {
                    module_code = data.module_code,
                    level = data.level,
                    request_path = data.path,
                    request_url = data.url,
                    method = data.method.ToString(),
                    message = data.message,
                    ip_address = data.ip,
                    log_create_time = DateTime.Now
                };
                await Manager.Log(logModel.ToJson());

                // add statistic log
                var statisticLogModel = new AddStatisticLogModel()
                {
                    module_code = data.module_code,
                    request_page = data.path,
                    action = data.action,
                    request_url = data.url,
                    request_time = DateTime.Now
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
