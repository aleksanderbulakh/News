using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace News
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
               name: "Home",
               url: "",
               defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional }
           );


            routes.MapRoute(
                name: "admin-redirect",
                url: "admin-redirect",
                defaults: new { controller = "PersonalArea", action = "AdminHomeAction", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "editor-redirect",
                url: "editor-redirect",
                defaults: new { controller = "PersonalArea", action = "EditorHomeAction", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "journalist-redirect",
                url: "journalist-redirect",
                defaults: new { controller = "PersonalArea", action = "JournalistHomeAction", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "news-created",
                url: "news-created",
                defaults: new { controller = "News", action = "Yeah", id = UrlParameter.Optional}    
            );

            routes.MapRoute(
                name: "new-user",
                url: "new-user",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional }
            );
            
            routes.MapRoute(
                name: "logof",
                url: "logof", 
                defaults: new { controller = "Account", action = "LogOff", id=UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "login",
                url: "login",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "personal-page",
                url: "personal-page",
                defaults: new { controller = "PersonalArea", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "new-add",
                url: "new-add",
                defaults: new { controller = "AddNews", action = "AddNew", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "news/item/",
                url: "news/item/{id}",
                defaults: new { controller = "MoreInfo", action = "MoreInfo", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "news/item/edit",
               url: "news/item/edit/{id}",
               defaults: new { controller = "EditNews", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "news/item/delete",
                url: "news/item/delete/{id}",
                defaults: new { controller = "DeleteNews", action = "DeleteNews", id = UrlParameter.Optional }
            );
        }
    }
}
