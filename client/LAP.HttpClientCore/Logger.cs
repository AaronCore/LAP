using System;
using System.IO;

namespace LAP.HttpClientCore
{
    /// <summary>
    /// 日志类库
    /// </summary>
    public class Logger
    {
        private static readonly object LogWriteLockObj = new object();
        /// <summary>
        /// 自定义日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="folder"></param>
        public static void Write(object message, string folder = "Logs")
        {
            WriteLog(message, folder);
        }
        private static void WriteLog(object message, string folder)
        {
            lock (LogWriteLockObj)
            {
                var dt = DateTime.Now;
                try
                {
                    var dir = $@"{AppDomain.CurrentDomain.BaseDirectory}\LAP.HttpClient.Logs\{folder}";
                    //var dir = $@"{Environment.CurrentDirectory}\LAP.HttpClient.Logs\{folder}";
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    var path = Path.Combine(dir + @"\" + dt.ToString("yyyy-MM-dd") + ".log");
                    using (var sw = new StreamWriter(path, true))
                    {
                        sw.WriteLine("{0:yyyy-MM-dd HH:mm:ss} | {1}", dt, message);
                        sw.Close();
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }
}
