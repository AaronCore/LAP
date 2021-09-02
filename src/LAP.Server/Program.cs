using System;
using System.Threading.Tasks;
using LAP.Common;

namespace LAP.Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("start server：");
            try
            {
                // 消息队列
                await RabbitMQMessage.Subscribe();
                // 定时任务
                await QuartzJobManager.JobRun();
            }
            catch (Exception e)
            {
                NLogger.Error(e);
            }
            Console.ReadKey();
        }
    }
}