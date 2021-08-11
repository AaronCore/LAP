using System;
using System.Threading.Tasks;
using LAP.Common;
using LAP.EntityFrameworkCore.Application;
using LAP.EntityFrameworkCore.Enum;
using LAP.EntityFrameworkCore.Model;
using LAP.EntityFrameworkCore.ViewModel;
using LAP.RabbitMQ;

namespace LAP.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start receive message：");
            try
            {
                var sendModel = new MessageArgs()
                {
                    SendEnum = SendEnum.订阅模式,
                    ExchangeName = MessageSettings.RabbitMQSetting.ExchangeName,
                    RabbitQueueName = MessageSettings.RabbitMQSetting.RabbitQueueName,
                    RouteName = MessageSettings.RabbitMQSetting.RouteName,
                };
                RabbitMQManage.Subscribe<MessageConsume>(sendModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Console.ReadKey();
        }
    }
    public class MessageConsume : IMessageConsume
    {
        private static readonly LogService LogService = new();
        private static readonly StatisticLogService StatisticLogService = new();

        public void Consume(string message)
        {
            var messageModel = message.ToObject<MessageModel>();
            NLogger.Write(messageModel.ToJson());

            switch (messageModel.type)
            {
                case MessageType.日志:
                    var logModel = messageModel.message.ToObject<LogInputDto>();
                    Task.Run(async () => await LogService.InsterLog(logModel));
                    break;
                case MessageType.请求日志:
                    var statisticModel = messageModel.message.ToObject<StatisticLogInputDto>();
                    Task.Run(async () => await StatisticLogService.InsterStatisticLog(statisticModel));
                    break;
            }
        }
    }
}