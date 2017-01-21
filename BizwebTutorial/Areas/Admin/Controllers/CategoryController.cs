using System.Web.Mvc;
using Models.Dao;
using Models.ViewModel;
using Models.EF;
using System.Web;
using System.Linq;

namespace BizwebTutorial.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryDao _CategoryService = new CategoryDao();
        private CollectionDao _collectionService = new CollectionDao();
        private ProductDao _productService = new ProductDao();
        private BiMartDbContext Dbcontext = new BiMartDbContext();
        public ActionResult Index(string sortname, string searchstring, string curentfillter, int? page)
        {
            ViewBag.SortName = string.IsNullOrEmpty(sortname) ? "NameSort" : "";
            var listcategory = _CategoryService.GetallCategory(sortname, searchstring, curentfillter, page);
            ViewBag.CurentSortOrder = sortname;
            ViewBag.CurentSearch = searchstring;
            return View(listcategory);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int Id)
        {
            var category = _CategoryService.ViewDetails(Id);
            var model = new CategoryEditModel()
            {
                Alias = category.Alias,
                Name=category.Name,
                Image=category.Image,
                Id=category.Id,
                MetaTittle=category.MetaTittle,
                Description=category.Description,
                IsVisible=category.IsVisible,
                MetaDiscription=category.MetaDiscription,
            };
            var collectionselect= _collectionService.GetByCategoryId(Id);
            var productselect = _productService.GetListProductIncollect(collectionselect.Select(c=>c.ProductId).ToList());
            foreach(var item in collectionselect)
            {
                var product = productselect.Where(c => c.Id == item.ProductId).SingleOrDefault();
                var imagefirst = Dbcontext.ImagePaths.Where(s => s.ProductId == product.Id).FirstOrDefault();
                var temp = new ProductMappingModel()
                {
                    CategoryId = item.CategoryId,
                    ProductId = item.ProductId,
                };
                if (product != null)
                {
                    temp.ProductImage = imagefirst != null ? imagefirst.PathImage : ("~/Upload/No_image.png");
                    temp.ProductName = product.Name;
                }
                model.ListProduct.Add(temp);
                }   
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            var category = _CategoryService.DeleteByID(id);
            var resulr = _collectionService.DeleteBycategoryID(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryModel entity, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(Server.MapPath("~/Upload/" + file.FileName));
                    entity.Image = "~/Upload/" + file.FileName;
                }
                var result = _CategoryService.Insert(entity);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Thẻ định danh đã  tồn tại");
                }
            }
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryEditModel entity, HttpPostedFileBase file)
        {
                if (file != null)
                {
                    file.SaveAs(Server.MapPath("~/Upload/" + file.FileName));
                    entity.Image = "~/Upload/" + file.FileName;
                }
                var result = _CategoryService.Update(entity);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Thẻ định danh đã tồn tại");
                }
            return View(entity);
        }
        public ActionResult ProductShow(string sortname, string searchstring, string curentfillter, int? page)
        {
            ViewBag.SortName = string.IsNullOrEmpty(sortname) ? "NameSort" : "";
            var listproduct = _CategoryService.GetMiniIndexProduct(sortname, searchstring, curentfillter, page);
            ViewBag.CurentSortOrder = sortname;
            ViewBag.CurentSearch = searchstring;
            return PartialView(listproduct);
        }
        public ActionResult AddProduct( int categoryId,int productId)
        {
                    _collectionService.Add(categoryId,productId);
            return Json("Ok", JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteProduct(int categoryId, int productId)
        {
            _collectionService.Delete(categoryId, productId);
            return Json("Ok", JsonRequestBehavior.AllowGet);
        }
    }
}