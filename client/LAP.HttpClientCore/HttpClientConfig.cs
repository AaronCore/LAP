namespace LAP.HttpClientCore
{
    public class HttpClientConfig
    {
        public static string ClientUrl = "http://10.10.1.22:801";

        public static string AddLogUrl = $"{ClientUrl}/api/log/addlog";

        public static string AddStatisticLogUrl = $"{ClientUrl}/api/statisticlog/addstatisticlog";
    }
}
