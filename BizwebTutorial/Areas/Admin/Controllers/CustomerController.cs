using System.Web.Mvc;
using Models.EF;
using Models.ViewModel;
using Models.Dao;
using System.Linq;
namespace BizwebTutorial.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerDao _CustomerService = new CustomerDao();
        private BiMartDbContext dbcontext = new BiMartDbContext();
        public ActionResult Index(string sortname, string searchstring, string curentfillter, int? page)
        {
            ViewBag.SortName = string.IsNullOrEmpty(sortname) ? "NameSort" : "";
            var listCustomer = _CustomerService.GetallCustormer(sortname, searchstring, curentfillter, page);
            ViewBag.CurentSortOrder = sortname;
            ViewBag.CurentSearch = searchstring;
            return View(listCustomer);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int Id)
        {
            var customerdb = _CustomerService.FindIdCustormer(Id);
            var cusmodel = new CustomerEditGetOrderModel()
            {
                Marketing=customerdb.Marketing,
                Address=customerdb.Address,
                City=customerdb.City,
                Email=customerdb.Email,
                Note=customerdb.Note,
                Tag=customerdb.Tag,
                Company=customerdb.Company,
                Country=customerdb.Country,
                FirstName=customerdb.FirstName,
                Id=customerdb.Id,
                Name=customerdb.Name,
                Phone=customerdb.Phone,
                Township=customerdb.Township,
                PostalCode=customerdb.PostalCode
            };
            var ordersellect = _CustomerService.GetOrderByIdCustormer(Id);
            foreach ( var item in ordersellect)
            {
                var temp = new OrderMapingCustomer()
                {
                    Idcustomer =item.CustomerId,
                    IdOrder=item.Id,
                    TotalMoney=item.TotalMoney,
                    CreateOn=item.CreatedOn,
                    NameOrder=item.Name
                };
                cusmodel.ListOrders.Add(temp);
            }
            return View(cusmodel);
        }
        public ActionResult EditPrivateInfor(int Id)
        {
            var customer = _CustomerService.FindIdCustormer(Id);
            return View(customer);
        }
        public ActionResult EditAddressCustomer(int Id)
        {
            var customer = _CustomerService.FindIdCustormer(Id);
            return View(customer);
        }
        public ActionResult Delete(int Id)
        {
            var customer = _CustomerService.Delete(Id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerModel entity)
        {
            if (ModelState.IsValid)
            {
                var result = _CustomerService.Insert(entity);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Email đã tồn tại ");
                }
            }
            return View(entity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerEditGetOrderModel entity)
        {
            if (ModelState.IsValid)
            {
                var result = _CustomerService.Update(entity);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Lưu không thành công");
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPrivateInfor(Customer entity)
        {
            if (ModelState.IsValid)
            {
                var result = _CustomerService.UpdateCustormerInformation(entity);
                return RedirectToAction("Edit/" + entity.Id);
            }
            return View(entity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAddressCustomer(Customer entity)
        {
            var result = _CustomerService.UpdateAddressCustomer(entity);
            if (result==true)
            {
                return RedirectToAction("Edit/" + entity.Id);
            }
            else
            {
                ModelState.AddModelError("","Sửa sản phâm không thành công");
            }
            return RedirectToAction("Edit/" + entity.Id);
        }
    }
}