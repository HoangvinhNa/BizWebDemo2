using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.EF;
using Models.Dao;
using System.Web.Mvc;
using BizwebTutorial.Dao;
using BizwebTutorial.Models;
using Newtonsoft.Json;

namespace BizwebTutorial.Controllers
{
    public class OrderViewController : Controller
    {
        private OrderDaoView _OrderService = new OrderDaoView();
        private BiMartDbContext Dbcontext = new BiMartDbContext();
        // GET: OrderView
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            HttpCookie Cookie_Customer = HttpContext.Request.Cookies["CUSTOMER_COOKIE"];
            HttpCookie Cookie_Product = HttpContext.Request.Cookies["CartCookie"];

            var valueProductCookie = Server.UrlDecode(Cookie_Product.Value);
            var listProduct = JsonConvert.DeserializeObject<List<CartViewModel>>(valueProductCookie);
            var lineProduct = JsonConvert.SerializeObject(listProduct);
            if (Cookie_Customer != null)
            {
                var valueCustomerCookie = Server.UrlDecode(Cookie_Customer.Value);
                var modelLoading = JsonConvert.DeserializeObject<CustomerLoginModel>(valueCustomerCookie);
                var model = new OrderModelView()
                {
                    CustomerId = modelLoading.ID,
                    EmailCustomer = modelLoading.Email,
                    NameCustomer = modelLoading.Name,
                    AddressCustomer = modelLoading.Address,
                    PhoneCustomer = modelLoading.Phone,
                    ListItemProduct = listProduct,
                    LineItemString = lineProduct,
                    Payment = true
                };
                return View(model);
            }
            else
            {
                var model1 = new OrderModelView()
                {
                    ListItemProduct = listProduct,
                    LineItemString = lineProduct,
                    Payment = true
                };
                return View(model1);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderModelView entity)
        {
            HttpCookie Cookie_Customer = HttpContext.Request.Cookies["CUSTOMER_COOKIE"];
            HttpCookie Cookie_Product = HttpContext.Request.Cookies["CartCookie"];
            if (Cookie_Customer != null)
            {
                if (ModelState.IsValid)
                {
                    var customer = Dbcontext.Customers.Find(entity.CustomerId);
                    customer.Address = entity.AddressCustomer;
                    customer.Phone = entity.PhoneCustomer;
                    customer.Name = entity.NameCustomer;
                    customer.Email = entity.EmailCustomer;
                    Dbcontext.SaveChanges();
                    var result = _OrderService.Insert(entity);
                    var dexoa = new HttpCookie("CartCookie");
                    dexoa.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(dexoa);
                    if (result>0)
                    {
                        return RedirectToAction("Confirm");
                    }
                    else
                    {
                        ModelState.AddModelError("","Đặt Hàng Không thành công ");
                    }
                }
                return View(entity);
            }
            else
            {
                if (ModelState.IsValid)
                {

                    var idcus = _OrderService.InsertCus(entity);
                    if (idcus > 0)
                    {
                        entity.CustomerId = idcus;
                        var resultorder = _OrderService.Insert(entity);
                        if (resultorder > 0)
                        {
                            var dexoa = new HttpCookie("CartCookie");
                            dexoa.Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies.Add(dexoa);
                            return RedirectToAction("Confirm");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Đặt Hàng Không thành công ");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email Đã bị trùng hoặc sai định dạng ");
                    }
                }
                return View(entity);
            }
        }
        public ActionResult Confirm()
        {
            return PartialView();
        }
    }
}