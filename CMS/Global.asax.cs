using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CMS.Models;
using CMS.Models.ModelBinders;

namespace CMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Add(typeof(Post), new PostModelBinder());
        }
    }
}
