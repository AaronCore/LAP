using System.Collections.Generic;

namespace LAP.EntityFrameworkCore.ViewModel
{
    public class LogChartDto
    {
        public string[] module { get; set; }
        public string[] xAxis { get; set; }
        public List<LogChartDataDto> data { get; set; }
    }
    public class LogChartDataDto
    {
        public string name { get; set; }
        public int[] data { get; set; }
    }

    public class StatisticLogChartDto
    {
        public string[] module { get; set; }
        public List<StatisticLogChartDataDto> series_data { get; set; }
        public List<StatisticLogChartDataDto> data { get; set; }
    }

    public class StatisticLogChartDataDto
    {
        public string name { get; set; }
        public int value { get; set; }
    }
}
