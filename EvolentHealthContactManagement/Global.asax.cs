using EvolentHealthContactManagement.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace EvolentHealthContactManagement
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperConfig.Initialize();
            GlobalConfiguration.Configuration.Filters.Add(new ValidateModelStateAttribute());
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
