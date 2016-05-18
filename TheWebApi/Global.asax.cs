using System.ServiceModel;
using System.Web;
using System.Web.Http;
using Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TheClientProxy;

namespace TheWebApi
{
    public class WebApiApplication : HttpApplication, ILongRunningCallback
    {
        public static bool Cancel { get; set; }
        public static LongRunningDuplexClientProxy SessionProxy { get; set; }
        public static decimal PercentageCompleted { get; set; }

        protected void Application_Start()
        {
            SessionProxy = new LongRunningDuplexClientProxy(new InstanceContext(this));
            GlobalConfiguration.Configure(config =>
            {
                config.Formatters.Remove(config.Formatters.XmlFormatter);

                var jsonFormatter = config.Formatters.JsonFormatter;
                jsonFormatter.Indent = true;

                var serializerSettings = jsonFormatter.SerializerSettings;
                serializerSettings.NullValueHandling = NullValueHandling.Include;
                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                
                config.MapHttpAttributeRoutes();
                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
            });
        }


        public bool ReportPercentage(decimal percentage)
        {
            PercentageCompleted = percentage;
            bool requestCancellationOfLongRunningProcess = Cancel;
            return requestCancellationOfLongRunningProcess;
        }
    }
}
