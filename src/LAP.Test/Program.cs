using System;
using System.Threading.Tasks;
using LAP.EntityFrameworkCore.Application;
using WeihanLi.Npoi;

namespace LAP.Test
{
    internal class Program
    {
        private static readonly ReportService ReportService = new();
        private static async Task Main(string[] args)
        {
            try
            {
                var sql = "SELECT * FROM `logs` ORDER BY created_time DESC LIMIT 100;";
                var dt = await ReportService.Query(sql);
                dt.ToExcelFile("C:\\Temp\\a.xlsx");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();
        }
    }
}
