using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using LAP.HttpClient.Enum;

namespace LAP.HttpClient.Test
{
    public class SeedData
    {
        private static readonly int[] MoudleCode = { 102, 103, 104, 105, 106, 107, 107, 109 };
        public static Log CreateLogData()
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
                    //.RuleFor(x => x.date, z => z.Date.Soon())
                    .RuleFor(x => x.date, z => z.Date.Recent())
                    //.RuleFor(x => x.date, z => z.Date.Between(Convert.ToDateTime("2021-01-01"), Convert.ToDateTime("2021-08-04")))
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
