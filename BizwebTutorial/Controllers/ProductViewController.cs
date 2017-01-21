using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BizwebTutorial.Dao;
using BizwebTutorial.Models;
using Newtonsoft.Json;

namespace BizwebTutorial.Controllers
{
    public class ProductViewController : Controller
    {
        private ProductViewDao _ProductViewService = new ProductViewDao();
        // GET: ProductView
        public ActionResult Index(string sortname, string searchstring, string curentfillter, int? page)
        {
            ViewBag.SortByNameDown = string.IsNullOrEmpty(sortname) ? "AZ" : "";
            var model = _ProductViewService.GetAllProductToView(sortname, searchstring, curentfillter, page);
            ViewBag.CurentSortOrder = sortname;
            ViewBag.CurentSearch = searchstring;
            return View(model);
        }
        [ChildActionOnly]
        public PartialViewResult ListProductByType()
        {
            var model = _ProductViewService.GetProductByType();
            return PartialView(model);
        }
        public PartialViewResult ListProductMachine()
        {
            var model = _ProductViewService.GetProductOfMachine();
            return PartialView(model);
        }
        public PartialViewResult ListProductGift()
        {
            var model = _ProductViewService.GetProductOfGift();
            return PartialView(model);
        }

        public ActionResult Details(string alias)
        {
            var modelproduct = _ProductViewService.getproductbyAlias(alias); 
            return View(modelproduct);
        }

        [ChildActionOnly]
        public ActionResult AddDetail()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDetail(CartViewModel entity)
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
            return RedirectToAction("DisplayCart", "ShopingCart");
        }
    }
}