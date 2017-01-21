using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BizwebTutorial.Models;
using Newtonsoft.Json;

namespace BizwebTutorial.Controllers
{
    public class ShopingCartController : Controller
    {
        [ChildActionOnly]
        public ActionResult AddCart()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCart(CartViewModel entity)
        {
            List<CartViewModel> cartViewModel = new List<CartViewModel>();
            HttpCookie Cookiecart = HttpContext.Request.Cookies["CartCookie"];
            if (Cookiecart != null)
            {
                var valuecookie = Server.UrlDecode(Cookiecart.Value);
                cartViewModel = JsonConvert.DeserializeObject<List<CartViewModel>>(valuecookie);
                var cartExist = cartViewModel.Where(s => s.Id == entity.Id).SingleOrDefault();
                if (cartExist != null)
                {
                    var ItemJson = JsonConvert.SerializeObject(cartViewModel, Formatting.Indented);
                    HttpCookie cookie = new HttpCookie("CartCookie", ItemJson);
                    cookie.Expires.AddDays(2);
                    HttpContext.Response.Cookies.Add(cookie);
                }
                else
                {
                    var Cartitem = new CartViewModel()
                    {
                        Id = entity.Id,
                        ImageProduct = entity.ImageProduct,
                        NameProduct = entity.NameProduct,
                        PriceProduct = entity.PriceProduct,
                        QuantityProduct = entity.QuantityProduct
                    };
                    cartViewModel.Add(Cartitem);
                    var ItemJson = JsonConvert.SerializeObject(cartViewModel, Formatting.Indented);
                    HttpCookie cookie = new HttpCookie("CartCookie", ItemJson);
                    cookie.Expires.AddDays(2);
                    HttpContext.Response.Cookies.Add(cookie);
                }
            }
            else
            {
                var Cartitem = new CartViewModel()
                {
                    Id = entity.Id,
                    ImageProduct = entity.ImageProduct,
                    NameProduct = entity.NameProduct,
                    PriceProduct = entity.PriceProduct,
                    QuantityProduct = entity.QuantityProduct
                };
                cartViewModel.Add(Cartitem);
                var ItemJson = JsonConvert.SerializeObject(cartViewModel, Formatting.Indented);
                HttpCookie cookie = new HttpCookie("CartCookie", ItemJson);
                cookie.Expires.AddDays(2);
                HttpContext.Response.Cookies.Add(cookie);
            }
            return RedirectToAction("DisplayCart");
        }
        public ActionResult DisplayCart()
        {
            List<CartViewModel> cartViewModel = new List<CartViewModel>();
            HttpCookie Cookie = HttpContext.Request.Cookies["CartCookie"];
            if (Cookie != null)
            {
                string ValueCookie = Server.UrlDecode(Cookie.Value);
                ViewBag.ValueCookie = ValueCookie;
                cartViewModel = JsonConvert.DeserializeObject<List<CartViewModel>>(ValueCookie);
            }
            return View(cartViewModel);
        }
        [HttpPost]
        public ActionResult DisplayCart(List<CartViewModel> model)
        {
            
            HttpCookie Cookiecart = HttpContext.Request.Cookies["CartCookie"];
            if (Cookiecart != null)
            {
                var c = new HttpCookie("CartCookie");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
                var ItemJson = JsonConvert.SerializeObject(model, Formatting.Indented);
                HttpCookie cookie = new HttpCookie("CartCookie", ItemJson);
                cookie.Expires.AddDays(2);
                HttpContext.Response.Cookies.Add(cookie);
                return RedirectToAction("DisplayCart");
            }
            return View(model);
        }
        public ActionResult Deleteproduct(int Id)
        {
            List<CartViewModel> cartViewModel = new List<CartViewModel>();
            HttpCookie Cookiecart = HttpContext.Request.Cookies["CartCookie"];
            if (Cookiecart != null)
            {
                var valuecookie = Server.UrlDecode(Cookiecart.Value);
                cartViewModel = JsonConvert.DeserializeObject<List<CartViewModel>>(valuecookie);
                var catitem = cartViewModel.Where(s => s.Id == Id).SingleOrDefault();
                cartViewModel.Remove(catitem);
                var ItemJson = JsonConvert.SerializeObject(cartViewModel, Formatting.Indented);
                HttpCookie cookie = new HttpCookie("CartCookie", ItemJson);
                cookie.Expires.AddDays(2);
                HttpContext.Response.Cookies.Add(cookie);
            }
            return RedirectToAction("DisplayCart");
        }
        public ActionResult DeleteAll()
        {
            if (Request.Cookies["CartCookie"] != null)
            {
                var c = new HttpCookie("CartCookie");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            return RedirectToAction("DisplayCart");
        }
    }
}

//public ActionResult xoacookei()
//{
//    if (Request.Cookies["MyCookie"] != null)
//    {
//        var c = new HttpCookie("MyCookie");
//        c.Expires = DateTime.Now.AddDays(-1);
//        Response.Cookies.Add(c);
//    }
//}