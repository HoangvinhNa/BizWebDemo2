using Models.Dao;
using Models.EF;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizwebTutorial.Areas.Admin.Controllers
{
    public class SLideController : Controller
    {
        private SlideImageDao _SlideService = new SlideImageDao();
        private BiMartDbContext DBcontext = new BiMartDbContext();
        // GET: Admin/SlideImage
        public ActionResult Index()
        {
            var listmodel = DBcontext.SlideImages.OrderByDescending(s => s.Id);
            return View(listmodel);
        }
        [HttpPost]
        public ActionResult Create()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        var path = Server.MapPath("~/SlideImages/" + file.FileName);
                        file.SaveAs(path);
                        var modelimage = new SlideImage()
                        {
                            ImageSlide = "~/SlideImages/" + file.FileName
                        };
                        DBcontext.SlideImages.Add(modelimage);
                        DBcontext.SaveChanges();
                    }
                    return Json("OK", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int Id)
        {
            _SlideService.DeleteImage(Id);
            return Json("Ok", JsonRequestBehavior.AllowGet);
        }
    }
}