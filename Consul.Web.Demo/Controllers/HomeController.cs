using Consul.Web.Demo.Common;
using Microsoft.AspNetCore.Mvc;

namespace Consul.Web.Demo.Controllers
{
    public class HomeController : Controller
    {
        public readonly ConsulClient ConsulClient;

        public HomeController(ConsulClient consulClient)
        {
            ConsulClient = consulClient;
        }

        public IActionResult Index()
        {
            var keyName = "testKey";
            ConsulClient.KvPut(keyName, "chengong");

            var value = ConsulClient.KvGet(keyName);
            return Content(value);
        }
    }
}