using System.Threading.Tasks;
using LAP.Common;
using LAP.EntityFrameworkCore.Enum;
using LAP.EntityFrameworkCore.Model;
using LAP.RabbitMQ;

namespace LAP.Client.Extensions
{
    public class RabbitMQMessage
    {
        public static async Task Send(RabbitMQMessageType type, string message)
        {
            var sendModel = new PublishMessage()
            {
                SendEnum = SendEnum.订阅模式,
                ExchangeName = MessageSettings.RabbitMqSetting.ExchangeName,
                RouteName = MessageSettings.RabbitMqSetting.RouteName,
                SendMessage = new RabbitMQMessageModel()
                {
                    type = type,
                    message = message
                }
            };
            await RabbitMQManage.PublishMessageAsync(sendModel);
        }
    }
}
