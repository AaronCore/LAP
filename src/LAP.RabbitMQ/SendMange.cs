using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;

namespace LAP.RabbitMQ
{
    internal class SendMange : ISend
    {
        public void SendMessage(PublishMessage message, IBus bus)
        {
            //一对一推送
            var msg = new Message<object>(message.SendMessage);
            IExchange ex = null;

            //判断推送模式
            switch (message.SendEnum)
            {
                case SendEnum.推送模式:
                    ex = bus.Advanced.ExchangeDeclare(message.ExchangeName, ExchangeType.Direct);
                    break;
                case SendEnum.订阅模式:
                    //广播订阅模式
                    ex = bus.Advanced.ExchangeDeclare(message.ExchangeName, ExchangeType.Fanout);
                    break;
                case SendEnum.主题路由模式:
                    //主题路由模式
                    ex = bus.Advanced.ExchangeDeclare(message.ExchangeName, ExchangeType.Topic);
                    break;
            }

            bus.Advanced.Publish(ex, message.RouteName, false, msg);
        }

        public async Task SendMessageAsync(PublishMessage message, IBus bus)
        {
            //一对一推送
            var msg = new Message<object>(message.SendMessage);
            IExchange ex = null;

            //判断推送模式
            switch (message.SendEnum)
            {
                case SendEnum.推送模式:
                    ex = await bus.Advanced.ExchangeDeclareAsync(message.ExchangeName, ExchangeType.Direct);
                    break;
                case SendEnum.订阅模式:
                    //广播订阅模式
                    ex = await bus.Advanced.ExchangeDeclareAsync(message.ExchangeName, ExchangeType.Fanout);
                    break;
                case SendEnum.主题路由模式:
                    //主题路由模式
                    ex = await bus.Advanced.ExchangeDeclareAsync(message.ExchangeName, ExchangeType.Topic);
                    break;
            }

            await bus.Advanced.PublishAsync(ex, message.RouteName, false, msg).ContinueWith(task =>
            {
                if (!task.IsCompleted && task.IsFaulted) //消息投递失败
                {
                    //记录投递失败的消息信息
                }
            });
        }
    }
}
