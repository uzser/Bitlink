using Bitlink.Web.Mappings;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json.Serialization;

namespace Bitlink.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // AutoMapper
            AutoMapperConfiguration.Configure();
            // Autofac
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
            // JSONFormat: js naming convention
            GlobalConfiguration.Configuration
               .Formatters
               .JsonFormatter
               .SerializerSettings
               .ContractResolver = new CamelCasePropertyNamesContractResolver();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
