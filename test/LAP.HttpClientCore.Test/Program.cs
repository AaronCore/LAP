using System;
using System.Net;
using System.Net.NetworkInformation;

namespace LAP.HttpClientCore.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new Uri("https://irp.colorful.cn/login/index").DnsSafeHost;
            Ping ping = new Ping();
            Console.WriteLine(ping.Send(host)?.Status);
            Console.Read();
            //var t1 = Convert.ToDateTime("2021-08-09 00:00:00");
            //var t2 = Convert.ToDateTime("2021-08-09 00:00:00");
            //Console.WriteLine(DateTime.Compare(t1, t2));
        }
    }
}
