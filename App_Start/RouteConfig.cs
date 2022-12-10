using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BoitMail
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Mails",
                url: "{controller}/{action}/Mails",
                defaults: new { controller = "Mails", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "ListMails",
               url: "{controller}/{action}/ListMails",
               defaults: new { controller = "ListMails", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
