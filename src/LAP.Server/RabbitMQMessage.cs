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
    /// <summary>
    /// 消息任务
    /// </summary>
    public class RabbitMQMessage
    {
        public static async Task Subscribe()
        {
            await Task.Run(() =>
             {
                 var sendModel = new MessageArgs()
                 {
                     SendEnum = SendEnum.订阅模式,
                     ExchangeName = MessageSettings.RabbitMqSetting.ExchangeName,
                     RabbitQueueName = MessageSettings.RabbitMqSetting.RabbitQueueName,
                     RouteName = MessageSettings.RabbitMqSetting.RouteName,
                 };
                 RabbitMQManage.Subscribe<MessageConsume>(sendModel);
             });
        }
    }
    public class MessageConsume : IMessageConsume
    {
        private static readonly LogService LogService = new();
        private static readonly StatisticLogService StatisticLogService = new();

        public void Consume(string message)
        {
            var messageModel = message.ToObject<RabbitMQMessageModel>();
            var messageJson = messageModel.ToJson();
            Console.WriteLine(messageJson);
            Console.WriteLine($"MessageMQ接收消息,在当前时间{DateTime.Now}---{messageJson}");
            // 消息类型处理
            switch (messageModel.type)
            {
                case RabbitMQMessageType.日志:
                    var logModel = messageModel.message.ToObject<LogInputDto>();
                    Task.Run(async () => await LogService.Inster(logModel));
                    break;
                case RabbitMQMessageType.请求日志:
                    var statisticModel = messageModel.message.ToObject<StatisticLogInputDto>();
                    Task.Run(async () => await StatisticLogService.Inster(statisticModel));
                    break;
            }
        }
    }
}
