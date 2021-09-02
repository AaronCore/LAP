using System;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;

namespace LAP.RabbitMQ
{
    public class RabbitMQManage
    {
        private static volatile IBus _bus;
        private static readonly object LockMq = new();

        /// <summary>
        /// 创建服务总线
        /// </summary>
        /// <returns></returns>
        public static IBus CreateEventBus()
        {
            var config = ConfigManage.GetInstance().AppSettings["RabbitMQ"];
            if (string.IsNullOrEmpty(config))
            {
                throw new Exception("消息地址未配置");
            }

            if (_bus == null && !string.IsNullOrEmpty(config))
            {
                lock (LockMq)
                {
                    if (_bus == null)
                    {
                        _bus = RabbitHutch.CreateBus(config);
                    }
                }
            }
            return _bus;
        }

        /// <summary>
        ///  消息同步投递
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool PublishMessage(PublishMessage message)
        {
            try
            {
                if (_bus == null)
                {
                    CreateEventBus();
                }

                new SendMange().SendMessage(message, _bus);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 消息异步投递
        /// </summary>
        /// <param name="message"></param>
        public static async Task PublishMessageAsync(PublishMessage message)
        {
            try
            {
                if (_bus == null)
                {
                    CreateEventBus();
                }

                await new SendMange().SendMessageAsync(message, _bus);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 消息订阅
        /// </summary>
        /// <typeparam name="TConsum"></typeparam>
        /// <param name="args"></param>
        public static void Subscribe<TConsum>(MessageArgs args) where TConsum : IMessageConsume, new()
        {
            if (_bus == null)
            {
                CreateEventBus();
            }

            if (string.IsNullOrEmpty(args.ExchangeName)) return;

            Expression<Action<TConsum>> methodCall;
            IExchange ex = null;
            //判断推送模式
            switch (args.SendEnum)
            {
                case SendEnum.推送模式:
                    ex = _bus.Advanced.ExchangeDeclare(args.ExchangeName, ExchangeType.Direct);
                    break;
                case SendEnum.订阅模式:
                    //广播订阅模式
                    ex = _bus.Advanced.ExchangeDeclare(args.ExchangeName, ExchangeType.Fanout);
                    break;
                case SendEnum.主题路由模式:
                    //主题路由模式
                    ex = _bus.Advanced.ExchangeDeclare(args.ExchangeName, ExchangeType.Topic);
                    break;
            }

            IQueue qu;
            if (string.IsNullOrEmpty(args.RabbitQueueName))
            {
                qu = _bus.Advanced.QueueDeclare();
            }
            else
            {
                qu = _bus.Advanced.QueueDeclare(args.RabbitQueueName);
            }

            _bus.Advanced.Bind(ex, qu, args.RouteName);
            _bus.Advanced.Consume(qu, (body, properties, info) => Task.Factory.StartNew(() =>
                {
                    try
                    {
                        lock (LockMq)
                        {
                            var message = Encoding.UTF8.GetString(body);
                            //处理消息
                            methodCall = job => job.Consume(message);
                            methodCall.Compile()(new TConsum());
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                })
            );
        }

        /// <summary>
        /// 释放服务总线
        /// </summary>
        public static void DisposeBus()
        {
            _bus?.Dispose();
        }
    }
}
