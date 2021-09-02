namespace LAP.HttpClient
{
    public class HttpClientConfig
    {
        public static string ClientUrl = "http://10.10.100.211:657";

        public static string AddLogUrl = $"{ClientUrl}/api/log/addlog";

        public static string AddStatisticLogUrl = $"{ClientUrl}/api/statisticlog/addstatisticlog";
    }
}
