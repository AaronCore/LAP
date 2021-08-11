using LAP.EntityFrameworkCore.Enum;

namespace LAP.EntityFrameworkCore.Model
{
    public class MqMessageModel
    {
        public MqMessageType type { get; set; }
        public string message { get; set; }
    }
}
