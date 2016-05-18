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
        public static State CurrentState { get; set; }

        protected void Application_Start()
        {
            ConfigureWebApi();

            SessionProxy = new LongRunningDuplexClientProxy(new InstanceContext(this));
            SessionProxy.Connect();
        }

        protected void Application_End()
        {
            SessionProxy.Disconnect();
            SessionProxy.Close();
        }

        public bool ReportState(State state)
        {
            CurrentState = state;
            bool requestCancellationOfLongRunningProcess = Cancel;
            return requestCancellationOfLongRunningProcess;
        }

        private static void ConfigureWebApi()
        {
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
                    defaults: new {id = RouteParameter.Optional}
                    );
            });
        }
    }
}
