using System;
using System.IO;
using System.Net;
using Consul.Demo.Common;
using Microsoft.Owin.Hosting;

namespace Consul.Demo
{
    public class MainService
    {
        private IDisposable _startup;
        private readonly ConsulClient _consulClient;
        private const string ServerId = "9652B5E3-E09C-4598-A7F6-46DAA0B98C09";
        private readonly Uri _consulUri;
        public MainService()
        {
            _consulUri = new Uri("ConsulHost".ValueOfAppSetting());
            _consulClient = new ConsulClient(a =>
            {
                a.Address = _consulUri;
                a.Datacenter = "dc1";
            });
        }

        public bool Start()
        {
            var address = "SelfHost".ValueOfAppSetting();

            _startup = WebApp.Start<Startup>(address);

            RegisterServer();

            Console.WriteLine("Owin服务已启动");

            return true;
        }

        public bool Stop()
        {
            _startup.Dispose();

            DeRegisterServer();

            Console.WriteLine("Owin服务已停止");

            return true;
        }

        private void RegisterServer()
        {
            var registerReuslt = _consulClient.Agent.ServiceRegister(new AgentServiceRegistration
            {
                Address = GetIp(),
                ID = ServerId,
                Name = "Consul.Demo",
                Port = _consulUri.Port,
                Check = new AgentServiceCheck
                {
                    HTTP =  "SelfHost".ValueOfAppSetting()+@"/api/home",
                    Interval = new TimeSpan(0, 0, 10),
                    DeregisterCriticalServiceAfter = new TimeSpan(0, 1, 0),
                }

            }).Result;

        }

        private void DeRegisterServer()
        {
            var deRegisterReuslt = _consulClient.Agent.ServiceDeregister(ServerId).Result;
        }

        private static string GetIp()
        {
            var hostName = Dns.GetHostName();
            var addressList = Dns.GetHostAddresses(hostName);
            return addressList[addressList.Length - 1].ToString();
        }
    }
}
