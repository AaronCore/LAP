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
        public static Log LogSeedData()
        {
            var log = new Faker<Log>()
                    .RuleFor(x => x.module_code, z => z.PickRandom(MoudleCode))
                    .RuleFor(x => x.site, z => z.Internet.Url())
                    .RuleFor(x => x.ip, z => z.Internet.Ip())
                    .RuleFor(x => x.path, z => z.Internet.UrlRootedPath())
                    .RuleFor(x => x.url, z => z.Internet.UrlWithPath())
                    .RuleFor(u => u.level, f => f.PickRandom<LogLevel>())
                    .RuleFor(u => u.action, f => f.PickRandom<StatisticAction>())
                    .RuleFor(u => u.method, f => f.PickRandom<Method>())
                    .RuleFor(x => x.exception, z => z.Lorem.Sentence())
                    .RuleFor(x => x.message, z => z.Lorem.Word())
                    .Generate(1);
            return log.First();
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
    }

    public enum Method
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}
