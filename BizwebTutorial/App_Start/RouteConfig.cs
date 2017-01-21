using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BizwebTutorial
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "khachdetails",
               url: "khach-hang/{Id}",
               defaults: new { controller = "CustomerLogin", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "Thanhcongdathang",
              url: "dat-hang-thanh-cong",
              defaults: new { controller = "OrderView", action = "Confirm"}
          );
            routes.MapRoute(
               name: "order",
               url: "dat-hang",
               defaults: new { controller = "OrderView", action = "Create" }
           );
            routes.MapRoute(
                 name: "Cart",
                 url: "gio-hang",
                 defaults: new { controller = "ShopingCart", action = "DisplayCart" }
             );
            routes.MapRoute(
                 name: "LoginRes",
                 url: "dang-ki",
                 defaults: new { controller = "CustomerLogin", action = "CustomerResgistration" }
             );
            routes.MapRoute(
                  name: "LoginCus",
                  url: "dang-nhap",
                  defaults: new { controller = "CustomerLogin", action = "Login" }
              );
            routes.MapRoute(
                  name: "About",
                  url: "gioi-thieu",
                  defaults: new { controller = "Home", action = "About" }
              );
            routes.MapRoute(
             name: "product-view",
             url: "san-pham/{alias}",
             defaults: new { controller = "productView", action = "Details", alias = UrlParameter.Optional }
         );
            routes.MapRoute(
                 name: "productall",
                 url: "tat-ca-san-pham",
                 defaults: new { controller = "productview", action = "Index" }
             );
            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Home", action = "Contact" }
            );
            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "CustomerLogin", action = "Login" }
            );
            
            routes.MapRoute(
               name: "Categoryview",
               url: "danh-muc/{alias}",
               defaults: new { controller = "CategoryView", action = "Index", alias = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
