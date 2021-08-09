using System;

namespace LAP.HttpClientCore.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var t1 = Convert.ToDateTime("2021-08-09 00:00:00");
            var t2 = Convert.ToDateTime("2021-08-09 00:00:00");
            Console.WriteLine(DateTime.Compare(t1, t2));
        }
    }
}
