using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BizwebTutorial.Models;
using BizwebTutorial.Dao;
using Models.Dao;
using Models.ViewModel;
using Newtonsoft.Json;
using BizwebTutorial.Dao;

namespace BizwebTutorial.Controllers
{
    public class CustomerLoginController : Controller
    {
        private CustomerViewDao _CustomerService = new CustomerViewDao();
        private CustomerDao _CustomerSeviceModels = new CustomerDao();
        // GET: CustomerLogin
        public ActionResult Index(int Id)//get all order of Cusomer to index 
        {
            var customerdb = _CustomerSeviceModels.FindIdCustormer(Id);
            var cusmodel = new CustomerEditGetOrderModel()
            {
                Address = customerdb.Address,
                City = customerdb.City,
                Password = customerdb.Password,
                Email = customerdb.Email,
                Note = customerdb.Note,
                Tag = customerdb.Tag,
                Company = customerdb.Company,
                Country = customerdb.Country,
                FirstName = customerdb.FirstName,
                Id = customerdb.Id,
                Name = customerdb.Name,
                Phone = customerdb.Phone,
                Township = customerdb.Township,
                PostalCode = customerdb.PostalCode
            };
            var ordersellect = _CustomerSeviceModels.GetOrderByIdCustormer(Id);
            foreach (var item in ordersellect)
            {
                var temp = new OrderMapingCustomer()
                {
                    Idcustomer = item.CustomerId,
                    IdOrder = item.Id,
                    TotalMoney = item.TotalMoney,
                    CreateOn = item.CreatedOn,
                    NameOrder = item.Name
                };
                cusmodel.ListOrders.Add(temp);
            }
            return View(cusmodel);
        }
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CustomerLoginModel entity)
        {
            if (ModelState.IsValid)
            {
                var result = _CustomerService.Login(entity.Email, Encryptor.MD5Hash(entity.Password));
                if (result)
                {
                    var customersa = _CustomerService.getbyId(entity.Email);
                    var modelcus = new CustomerLoginModel()
                    {
                        Email = customersa.Email,
                        ID = customersa.Id,
                        Address = customersa.Address,
                        Phone = customersa.Phone,
                        Password = customersa.Password,
                        Name = customersa.Name
                    };
                    var ItemCusCookie = JsonConvert.SerializeObject(modelcus, Formatting.Indented);
                    HttpCookie cookie = new HttpCookie("CUSTOMER_COOKIE", ItemCusCookie);
                    cookie.Expires.AddDays(15);
                    HttpContext.Response.Cookies.Add(cookie);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Ban sai email hoac mat khau");
                }
            }
            return View(entity);
        }
        public ActionResult CustomerResgistration()
        {
            return View();
        }
        public ActionResult Logout()
        {
            if (Request.Cookies["CUSTOMER_COOKIE"] != null)
            {
                var c = new HttpCookie("CUSTOMER_COOKIE");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerResgistration(CustomerLoginModel entity)
        {
            if (ModelState.IsValid)
            {
                var result = _CustomerService.CustomerRegis(entity);
                if (result > 0)
                {
                    var login = _CustomerService.Login(entity.Email, Encryptor.MD5Hash(entity.Password));
                    if (login)
                    {
                        var customersa = _CustomerService.getbyId(entity.Email);
                        var modelcus = new CustomerLoginModel()
                        {
                            Email = customersa.Email,
                            ID = customersa.Id,
                            Address = customersa.Address,
                            Phone = customersa.Phone,
                            Password = customersa.Password,
                            Name = customersa.Name
                        };
                        var ItemCusCookie = JsonConvert.SerializeObject(modelcus, Formatting.Indented);
                        HttpCookie cookie = new HttpCookie("CUSTOMER_COOKIE", ItemCusCookie);
                        cookie.Expires.AddDays(15);
                        HttpContext.Response.Cookies.Add(cookie);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng kí không thành công");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
            }
            return View(entity);
        }
    }
}