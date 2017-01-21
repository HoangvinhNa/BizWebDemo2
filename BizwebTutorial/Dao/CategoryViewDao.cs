using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.EF;

namespace BizwebTutorial.Dao
{
    public class CategoryViewDao
    {
        private BiMartDbContext Dbcontext = new BiMartDbContext();
       
        public IEnumerable<Category> ListAllCategory()
        {
            return Dbcontext.Categories.Where(x => x.IsVisible == true).OrderByDescending(x => x.Name).ToList();
        }
         public IEnumerable<Product> GetListProDuctOfCategory(List<int> IdsProduct)
        {
            return Dbcontext.Products.Where(x=>IdsProduct.Contains(x.Id)).OrderByDescending(x=>x.Name).ToList();
        }
    }
}