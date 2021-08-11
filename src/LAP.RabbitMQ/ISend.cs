using System.Threading.Tasks;
using EasyNetQ;

namespace LAP.RabbitMQ
{
    internal interface ISend
    {
        void SendMessage(PublishMessage message, IBus bus);
        Task SendMessageAsync(PublishMessage message, IBus bus);
    }
}
