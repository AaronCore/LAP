using System;
using System.Net.NetworkInformation;

namespace LAP.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var host = "10.10.100.201:8080";
                //var host = "https://irp.colorful1.cn/login/index";

                host = host.Replace("https://", "http://");
                host = host.StartsWith("http://") ? host : $"http://{host}";
                host = new Uri(host).DnsSafeHost;

                Console.WriteLine(host);

                Ping ping = new Ping();
                Console.WriteLine(ping.Send(host)?.Status);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();
        }
    }
}
