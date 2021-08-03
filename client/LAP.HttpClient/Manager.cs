using System;
using System.Threading.Tasks;

namespace LAP.HttpClient
{
    public class Manager
    {
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="contentJson">添加日志输入实体对象Json</param>
        /// <returns></returns>
        public static async Task Log(string contentJson)
        {
            try
            {
                await HttpHelper.PostAsync(HttpClientConfig.AddLogUrl, contentJson);
            }
            catch (Exception e)
            {
                Logger.Write(e.ToString(), "Log");
            }
        }

        /// <summary>
        /// 添加统计日志
        /// </summary>
        /// <param name="contentJson">添加统计日志对象对象Json</param>
        /// <returns></returns>
        public static async Task StatisticLog(string contentJson)
        {
            try
            {
                await HttpHelper.PostAsync(HttpClientConfig.AddLogUrl, contentJson);
            }
            catch (Exception e)
            {
                Logger.Write(e.ToString(), "StatisticLog");
            }
        }
    }
}
