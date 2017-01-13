using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace practiceA
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Invite",
                url: "{controller}/{action}/{nickname}/{color}",
                defaults: new { controller = "Calendar", action = "InviteCard", nickname = UrlParameter.Optional, color = UrlParameter.Optional }
            );
        }
    }
}
