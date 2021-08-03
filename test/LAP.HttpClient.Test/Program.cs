using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LAP.HttpClient.Model;

namespace LAP.HttpClient.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Add_Log_Test();

            Console.WriteLine("end...");
            //Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            //Console.WriteLine(Environment.CurrentDirectory);
        }

        /// <summary>
        /// Add_Log_Test
        /// </summary>
        /// <returns></returns>
        public static async Task Add_Log_Test()
        {
            var infoModel = new AddLogModel()
            {
                module_code = 101,
                level = Enum.LogLevel.Info,
                request_path = "/Home/Index",
                request_url = "/Home/Index",
                method = "GET",
                message = $"时间：{DateTime.Now}",
                ip_address = "127.0.0.1",
                log_create_time = DateTime.Now
            };
            await Manager.Log(infoModel.ToJson());
        }

        /// <summary>
        /// Add_StatisticLog_Test
        /// </summary>
        /// <returns></returns>
        public static async Task Add_StatisticLog_Test()
        {
            await Manager.StatisticLog("");
        }
    }
}
