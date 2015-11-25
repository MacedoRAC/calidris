﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FirstREST
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Picking", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Picking",
                 url: "{controller}/{action}/{id}",
                defaults: new { controller = "Picking", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Putaway",
                url: "{controller}",
                defaults: new { controller = "Putaway", id = UrlParameter.Optional }
            );
        }
    }
}