namespace LAP.Common
{
    public class MessageSettings
    {
        public static class RabbitMqSetting
        {
            public static string ExchangeName => "lap.log";
            public static string RabbitQueueName => "lap_log";
            public static string RouteName => "lap_log";
        }
    }
}
