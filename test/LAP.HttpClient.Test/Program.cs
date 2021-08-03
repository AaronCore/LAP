﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LAP.HttpClient.Enum;
using LAP.HttpClient.Model;

namespace LAP.HttpClient.Test
{
    class Program
    {
        private const int MODULE_CODE = 102;
        static async Task Main(string[] args)
        {
            //await Add_Log_Test();
            await Add_StatisticLog_Test();
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
                module_code = MODULE_CODE,
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
                module_code = MODULE_CODE,
                request_page = "Index页面",
                action = StatisticAction.页面访问,
                request_url = "http://10.10.1.22:3200/Home/Index",
                request_time = DateTime.Now
            };
            await Manager.StatisticLog(statisticLogModel.ToJson());
        }
    }
}
