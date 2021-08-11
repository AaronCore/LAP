using System.Collections.Specialized;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LAP.RabbitMQ
{
    public class ConfigManage
    {
        // 定义一个标识确保线程同步
        private static readonly object Locker = new();
        private static ConfigManage _configManage;
        private readonly string BaseFileName = "appsettings.json";
        private IConfigurationRoot ConfigOperat;

        private ConfigManage()
        {

        }

        public static ConfigManage GetInstance()
        {
            if (_configManage == null)
            {
                lock (Locker)//确保线程安全
                {
                    if (_configManage == null)
                    {
                        _configManage = new ConfigManage();
                        _configManage.ConfigOperat = new ConfigurationBuilder()
                        .AddInMemoryCollection()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(_configManage.BaseFileName, optional: true, reloadOnChange: true)
                        .Build();
                    }
                }
            }
            return _configManage;
        }

        public T Settings<T>(string key) where T : class, new()
        {
            var config = new ServiceCollection()
                .AddOptions()
                .Configure<T>(_configManage.ConfigOperat.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()?.Value;
            return config;
        }

        /// <summary>
        /// AppSettings配置信息
        /// </summary>
        public NameValueCollection AppSettings
        {
            get
            {
                var list = _configManage.ConfigOperat.GetSection("AppSettings").GetChildren().ToList();
                NameValueCollection dc = new NameValueCollection();
                foreach (var item in list)
                {
                    dc.Add(item.Key, item.Value);
                }
                return dc;
            }
        }
    }
}
