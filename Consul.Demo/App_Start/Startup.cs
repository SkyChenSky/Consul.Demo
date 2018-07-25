using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Converters;
using Owin;

namespace Consul.Demo.Common
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            var jsonFormatter = new JsonMediaTypeFormatter();
            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
            jsonFormatter.SerializerSettings.Converters.Add(
                new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" }
            );

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "Normal",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional, action = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }

    }
}
