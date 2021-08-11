namespace LAP.RabbitMQ
{
    public interface IMessageConsume
    {
        void Consume(string message);
    }
}
