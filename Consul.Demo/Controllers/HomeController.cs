using System;
using System.Web.Http;

namespace Consul.Demo.Controllers
{
    public class HomeController : ApiController
    {
        public DateTime Get()
        {
            return DateTime.Now;
        }
    }
}
