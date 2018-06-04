using Nancy.Hosting.Self;
using System;

namespace GTAVRewardHook
{
    /// <summary>
    /// Nancy 服务器
    /// </summary>
    class Server
    {
        public static HostConfiguration hostConfig = new HostConfiguration()
        {
            UrlReservations = new UrlReservations() { CreateAutomatically = true }
        };

        public static void StartHost()
        {
            //监听本地接口
            using (var host = new NancyHost(hostConfig, new Uri("http://localhost:31730")))
            {
                host.Start();
                while (true) ;
            }
        }
    }
}
