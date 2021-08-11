using LAP.EntityFrameworkCore.Enum;

namespace LAP.EntityFrameworkCore.Model
{
    public class MessageModel
    {
        public MessageType type { get; set; }
        public string message { get; set; }
    }
}
