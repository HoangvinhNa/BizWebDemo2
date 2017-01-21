using System.Web.Mvc;
using Models.Dao;
using Models.EF;
using Models.ViewModel;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;

namespace BizwebTutorial.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private ProductDao _ProductService = new ProductDao();
        private CollectionDao _CollectionDao = new CollectionDao();
        private CategoryDao _CategoryDao = new CategoryDao();
        private BiMartDbContext _dbcontext = new BiMartDbContext();
        // GET: Admin/Product
        [HttpGet]
        public ActionResult Index(string sortname, string searchstring, string curentfillter, int? page)
        {
            ViewBag.SortName = string.IsNullOrEmpty(sortname) ? "NameSort" : "";
            var listcategory = _ProductService.GetallProduct(sortname, searchstring, curentfillter, page);
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
            var product = _ProductService.FindById(Id);
            var modelproduct = new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Descripstion = product.Descripstion,
                ProductType = product.ProductType,
                Vendor = product.Vendor,
                Image = product.Image,
                ShortDescription=product.ShortDescription,
                Price = product.Price,
                OldPrice = product.OldPrice,
                Sku = product.Sku,
                Barcode = product.Barcode,
                MetaTitle = product.MetaTitle,
                MetaDescription = product.MetaDescription,
                Alias = product.Alias,
                IsVisible = product.IsVisible
            };
            var Imagepathmapping = _dbcontext.ImagePaths.Where(s => s.ProductId == Id).ToList();
            foreach (var item in Imagepathmapping)
            {
                var imagemodel = new ImagePathMapingProduct()
                {
                    Id = item.Id,
                    ImagePath = item.PathImage,
                    ProductId = item.ProductId
                };
                modelproduct.ListImagePath.Add(imagemodel);
            }
            var collectionsellect = _CollectionDao.GetByProductId(Id);
            var categorysellect = _CategoryDao.GetListCategoryInCollection(collectionsellect.Select(c => c.CategoryId).ToList());
            foreach (var item in collectionsellect)
            {
                var template = new CategoryMappingModel()
                {
                    CategoryId = item.CategoryId,
                    ProductId = item.ProductId,
                };
                var categoryes = categorysellect.Where(c => c.Id == item.CategoryId).SingleOrDefault();
                if (categoryes != null)
                {
                    template.CategoryImage = categoryes.Image;
                    template.CategoryName = categoryes.Name;
                }
                modelproduct.ListCategory.Add(template);
            }
            return View(modelproduct);
        }
        public ActionResult Delete(int Id)
        {
            var product = _ProductService.DeleteByID(Id);
            var result = _CollectionDao.DeleteByProductID(Id);
            var productimage = _dbcontext.ImagePaths.Where(s => s.ProductId == Id).ToList();
            foreach (var item in productimage)
            {
                _dbcontext.ImagePaths.Remove(item);
                _dbcontext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel entity, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                var resultId = _ProductService.Insert(entity);
                if (files != null && files.Count() > 0)
                {
                    foreach (var item in files)
                    {
                        if (item == null) continue;
                        var path = Server.MapPath("~/TF/" + item.FileName);
                        item.SaveAs(path);
                        var modelef = new ImagePath()
                        {
                            PathImage = "~/TF/" + item.FileName,
                            ProductId = resultId
                        };
                        _dbcontext.ImagePaths.Add(modelef);
                        _dbcontext.SaveChanges();
                    }
                }
                if (entity.CategoryIdsString != null)
                {
                    foreach (var item in entity.CategoryIdsString.Split(','))
                    {
                        var collect = new Collection()
                        {
                            CategoryId = int.Parse(item),
                            ProductId = resultId
                        };
                        _dbcontext.Collections.Add(collect);
                        _dbcontext.SaveChanges();
                    }
                }
                if (resultId>0)
                {
                    return RedirectToAction("Edit/" + resultId);
                }
                else
                {
                    ModelState.AddModelError("", "Thẻ định danh(Alias) bị trùng");
                }
            }
            return View(entity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductModel entity, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                var result = _ProductService.Update(entity);
                try
                {
                    if (files != null && files.Count() > 0)
                    {
                        foreach (var item in files)
                        {
                            if (item == null) continue;
                            var path = Server.MapPath("~/TF/" + item.FileName);
                            item.SaveAs(path);
                            var modelef = new ImagePath()
                            {
                                PathImage = "~/TF/" + item.FileName,
                                ProductId = entity.Id
                            };
                            _dbcontext.ImagePaths.Add(modelef);
                            _dbcontext.SaveChanges();
                        }
                    }
                }
                catch
                {
                }
                if (result)
                {
                   return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("","Thẻ định danh Alias bị trùng");
                }
            }
            return View(entity);
        }
        public ActionResult MinicategoryIndex(string sortname, string searchstring, string curentfillter, int? page)
        {
            ViewBag.SortName = string.IsNullOrEmpty(sortname) ? "NameSort" : "";
            var listcategory = _ProductService.GetminiCategory(sortname, searchstring, curentfillter, page);
            ViewBag.CurentSortOrder = sortname;
            ViewBag.CurentSearch = searchstring;
            return PartialView(listcategory);
        }
        public ActionResult DeleteImageOfProduct(int Id, int Idproduct)
        {
            var image = _dbcontext.ImagePaths.Where(s => s.Id == Id).SingleOrDefault();
            _dbcontext.ImagePaths.Remove(image);
            _dbcontext.SaveChanges();
            return Json("Ok", JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddImagetoListImage()
        {
            return View();
        }
    }
}