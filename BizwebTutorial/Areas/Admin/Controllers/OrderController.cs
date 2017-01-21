using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Dao;
using Models.EF;
using Models.ViewModel;
using Newtonsoft.Json;

namespace BizwebTutorial.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private OrderDao _orderService = new OrderDao();
        private BiMartDbContext _dbcontext = new BiMartDbContext();
        // GET: Admin/Order
        public ActionResult Index(string sortname, string searchstring, string curentfillter, int? page)
        {
            ViewBag.SortName = string.IsNullOrEmpty(sortname) ? "NameSort" : "";
            var Listorder = _orderService.GetOrderToIndex(sortname, searchstring, curentfillter, page);
            ViewBag.CurentSortOrder = sortname;
            ViewBag.CurentSearch = searchstring;
            return View(Listorder);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var orderentity = _dbcontext.Orders.Find(id);
            var nemodel = new OrderEditModel()
            {
                Id = orderentity.Id,
                Name = orderentity.Name,
                LineItems = orderentity.LineItems,
                Payment = orderentity.Payment,
                TotalMoney = orderentity.TotalMoney,
                CustomerId = orderentity.CustomerId,
                CustomerAdress = orderentity.CustomerAddress,
                CustomerEmail= orderentity.CustomerEmail,
                CustomerName= orderentity.CustomerName,
                CustomerPhone= orderentity.CustomerPhone,
                Note = orderentity.Note,
                Transport = orderentity.Transport,
                Items = JsonConvert.DeserializeObject<List<LineItemModel>>(orderentity.LineItems)
            };
            return View(nemodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderModel entity)
        {
            if (ModelState.IsValid)
            {
                var result = _orderService.InsertOrder(entity);
                if (result>0)
                {
                    return RedirectToAction("Edit/" + result);
                }
                else
                {
                    ModelState.AddModelError("","Thêm Đơn hàng Không thành công");
                }
                
            }
            return View(entity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderEditModel entity)
        {
            if (ModelState.IsValid)
            {
                var result = _orderService.Update(entity);
              
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm Đơn hàng Không thành công");
                }
            }
            return View(entity);
        }
        public ActionResult ProductForOrderView(string sortname, string searchstring, string curentfillter, int? page)
        {
            var listproduct = _orderService.GetProductForOrder(sortname, searchstring, curentfillter, page);
            return PartialView(listproduct);
        }
        public ActionResult CustomerForOrderView(string sortname, string searchstring, string curentfillter, int? page)
        {
            var listcustomer = _orderService.GetCustormerForOrder(sortname, searchstring, curentfillter, page);
            return PartialView(listcustomer);
        }
    }
}