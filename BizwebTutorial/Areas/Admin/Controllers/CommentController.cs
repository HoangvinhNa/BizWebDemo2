using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.EF;
using Models.ViewModel;
using Models.Dao;
namespace BizwebTutorial.Areas.Admin.Controllers
{
    public class CommentController : Controller
    {
        private CommentDao _serviceComment = new CommentDao();
        // GET: Admin/Comment
        public ActionResult Index(string sortname, string searchstring, string curentfillter, int? page)
        {
            ViewBag.SortName = string.IsNullOrEmpty(sortname) ? "NameSort" : "";
            var listComment = _serviceComment.GetallComment(sortname, searchstring, curentfillter, page);
            ViewBag.CurentSortOrder = sortname;
            ViewBag.CurentSearch = searchstring;
            return View(listComment);
        }
        public ActionResult Details(int Id)
        {
            var result = _serviceComment.DetailComment(Id);
            return View(result);
        }
    }
}