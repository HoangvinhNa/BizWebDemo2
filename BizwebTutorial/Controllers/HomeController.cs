using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.EF;
using Models.Dao;
using Models.ViewModel;

namespace BizwebTutorial.Controllers
{
    public class HomeController : Controller
    {
        private CommentDao _CommneSevice = new CommentDao();
        private BiMartDbContext _Dbcontect = new BiMartDbContext();
        public ActionResult Index()
        {
            var listmodel = _Dbcontect.SlideImages.OrderByDescending(s=>s.Id);
            return View(listmodel);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactUser entity)
        {
            var result = _CommneSevice.Insert(entity);
            return RedirectToAction("Contact");
        }
    }
}