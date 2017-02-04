using BizwebTutorial.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.EF;
using System.Web.Mvc;
using BizwebTutorial.Models;
namespace BizwebTutorial.Controllers
{
    public class CategoryViewController : Controller
    {
        private CategoryViewDao _categoryviewService = new CategoryViewDao();
        private CollectionViewDao _CollectionService = new CollectionViewDao();
        private BiMartDbContext Dbcontext = new BiMartDbContext();
        // GET: ProductView
        public ActionResult Index(string alias)
        {
            var DBcategory = Dbcontext.Categories.Where(s=>s.Alias==alias).SingleOrDefault();
            var category = DBcategory;
            var model = new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name
            };
            var collection = _CollectionService.GetCollectHasIdcategory(category.Id);
            var _product = _categoryviewService.GetListProDuctOfCategory(collection.Select(x => x.ProductId).ToList());
            foreach (var item in collection)
            {
                var product = _product.Where(c => c.Id == item.ProductId).SingleOrDefault();
                var imagefirst = Dbcontext.ImagePaths.Where(s => s.ProductId == product.Id).FirstOrDefault();
                var template = new ProductMaptoCategory()
                {
                    CategoryId=item.CategoryId,
                    ProductId=item.ProductId
                };
                if (product != null)
                {
                    template.ProductImage = imagefirst != null ? imagefirst.PathImage : ("~/Upload/No_image.png");
                    template.ProductName = product.Name;
                    template.Productprice = product.Price;
                    template.ProductAlias= product.Alias;
                    template.OldPrice = product.OldPrice;
                }
                model.ListProducts.Add(template);
            }
            return View(model);
        }
        [ChildActionOnly]
        public PartialViewResult CategoryView()
        {
            var model = _categoryviewService.ListAllCategory();
            return PartialView(model);
        }

    }
}