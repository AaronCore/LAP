using LAP.EntityFrameworkCore.Enum;

namespace LAP.EntityFrameworkCore.Model
{
    /// <summary>
    /// MQ消息实体
    /// </summary>
    public class RabbitMQMessageModel
    {
        public RabbitMQMessageType type { get; set; }
        public string message { get; set; }
    }
}
