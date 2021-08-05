using System;
using System.Threading.Tasks;

namespace LAP.HttpClient.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await LogSeedData.AddLog();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("end...");
            Console.ReadKey();
        }
    }
}
