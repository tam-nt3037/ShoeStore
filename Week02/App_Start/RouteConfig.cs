using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Week02
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // RentalProperties/Boardwalk/Units/4A
            //routes.MapRoute(
            //    name: "ProductDetail",
            //    url: "Home/ProductDetail/{id}/{Nsx_sp}",
            //    defaults: new
            //    {
            //        controller = "Home",
            //        action = "",
            //    }
            //);

            routes.MapRoute(
                name: "Details",
                url: "{controller}/{action}/{id}/{Nsx_sp}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional , Nsx_sp = UrlParameter.Optional}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
           // routes.MapRoute(
           //    name: "Login",
           //    url: "Dang-nhap",
           //    defaults: new { controller = "AccountController", action = "Login", id = UrlParameter.Optional },
           //    namespaces: new[] { "Week02.Controllers" }
           //);
        }
    }
}
