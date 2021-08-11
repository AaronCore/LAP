using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using LAP.HttpClient.Enum;
using LAP.HttpClient.Model;

namespace LAP.HttpClient.Test
{
    public class LogSeedData
    {
        /// <summary>
        /// add log
        /// </summary>
        /// <returns></returns>
        public static async Task AddLog()
        {
            for (int i = 1; i <= 10; i++)
            {
                Thread.Sleep(1000);

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{DateTime.Now}  第{i}次执行...");

                // 种子数据
                var logData = CreateLogData();

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
        }

        private static readonly int[] MoudleCode = { 101, 102, 103, 104, 105, 106, 107, 107, 109 };
        private static Log CreateLogData()
        {
            var log = new Faker<Log>()
                    .RuleFor(x => x.module_code, z => z.PickRandom(MoudleCode))
                    .RuleFor(x => x.site, z => z.Internet.Url())
                    .RuleFor(x => x.ip, z => z.Internet.Ip())
                    .RuleFor(x => x.path, z => z.Internet.UrlRootedPath())
                    .RuleFor(x => x.url, z => z.Internet.UrlWithPath())
                    .RuleFor(x => x.level, z => z.PickRandom<LogLevel>())
                    .RuleFor(x => x.action, z => z.PickRandom<StatisticAction>())
                    .RuleFor(x => x.method, z => z.PickRandom<Method>())
                    .RuleFor(x => x.exception, z => z.Lorem.Sentence())
                    .RuleFor(x => x.message, z => z.Lorem.Word())
                    //.RuleFor(x => x.date, z => z.Date.Recent())
                    .RuleFor(x => x.date, z => z.Date.Soon())
                    //.RuleFor(x => x.date, z => z.Date.Between(Convert.ToDateTime("2021-08-09 00:00:00"), Convert.ToDateTime("2021-08-09 23:59:59")))
                    .Generate();
            return log;
        }
    }

    public class Log
    {
        public int module_code { get; set; }
        public string site { get; set; }
        public string ip { get; set; }
        public string path { get; set; }
        public string url { get; set; }
        public LogLevel level { get; set; }
        public StatisticAction action { get; set; }
        public Method method { get; set; }
        public string exception { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }
    }

    public enum Method
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}
