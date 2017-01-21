
using Models.EF;
using Models.ProductModelFix;
using Models.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.Dao
{
    public class CategoryDao
    {
        private BiMartDbContext Dbcontext = new BiMartDbContext();
        public IEnumerable<Category> GetallCategory(string sortname, string searchstring, string curentfillter, int? page)
        {
            var category = from s in Dbcontext.Categories
                           select s;
            switch (sortname)
            {
                case "NameSort":
                    category = category.OrderByDescending(s => s.Name);
                    break;
                default:
                    category = category.OrderBy(s => s.Name);
                    break;
            }
            if (!string.IsNullOrEmpty(searchstring))
            {
                category = category.Where(s => s.Name.Contains(searchstring));
            }
            if (searchstring != null)
            {
                page = 1;
            }
            else
            {
                searchstring = curentfillter;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return category.ToPagedList(pageNumber, pageSize);
        }
        public int Insert(CategoryModel entity)
        {
            Category category = new Category();
            category.Name = entity.Name;
            category.Image = entity.Image;
            category.Alias = entity.Alias;
            category.Description = entity.Description;
            category.MetaTittle = entity.MetaTittle;
            category.MetaDiscription = entity.MetaDiscription;
            category.IsVisible = entity.IsVisible;
            category.CreatedOn = DateTime.Now;

            var checkalias = Dbcontext.Categories.Any(s=>s.Alias==entity.Alias);
            if (checkalias)
            {
                return 0;
            }
            else
            {
                Dbcontext.Categories.Add(category);
                Dbcontext.SaveChanges();
                return category.Id;
            }
        }
        public Category ViewDetails(int id)
        {
            return Dbcontext.Categories.Find(id);//tim kiem cat theo id
        }
        public bool Update(CategoryEditModel entity)
        {
            try
            {
                var category = Dbcontext.Categories.Find(entity.Id);
                var ListAlias = Dbcontext.Categories.Select(m => m.Alias).ToList();
                var Aliasentity = Dbcontext.Categories.Find(entity.Id).Alias.ToString();
                ListAlias.Remove(Aliasentity);
                var Lisst12 = ListAlias.AsEnumerable();
                var checkalias = Lisst12.Any(item => item == entity.Alias);

                category.Name = entity.Name;
                category.Image = entity.Image;
                category.ModifiedOn = DateTime.Now;
                category.MetaTittle = entity.MetaTittle;
                category.MetaDiscription = entity.MetaDiscription;
                category.IsVisible = entity.IsVisible;
                category.Alias = entity.Alias;
                category.Description = entity.Description;
                if (checkalias==true)
                {
                    return false;
                }
                else
                {
                    Dbcontext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteByID(int id)
        {
            Category category = Dbcontext.Categories.Find(id);
            Dbcontext.Categories.Remove(category);
            Dbcontext.SaveChanges();
            return true;
        }
        public IEnumerable<ProductListTbModel> GetMiniIndexProduct(string sortname, string searchstring, string curentfillter, int? page)
        {
            var product = from s in Dbcontext.Products
                          select s;
            var listmodel = new List<ProductListTbModel>();
            foreach (var item in product)
            {
                var imageFirst = Dbcontext.ImagePaths.Where(s => s.ProductId == item.Id).FirstOrDefault();
                var productmodel = new ProductListTbModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Image = imageFirst != null ? imageFirst.PathImage : ("~/Upload/No_image.png")
                };
                listmodel.Add(productmodel);
            }
            var models21 = listmodel.AsEnumerable();
            switch (sortname)
            {
                default:
                    models21 = models21.OrderByDescending(s => s.Name);
                    break;
            }
            if (!string.IsNullOrEmpty(searchstring))
            {
                models21 = models21.Where(s => s.Name.Contains(searchstring));
            }
            if (searchstring != null)
            {
                page = 1;
            }
            else
            {
                searchstring = curentfillter;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return models21.ToPagedList(pageNumber, pageSize);
        }
        public IEnumerable<Category> GetListCategoryInCollection(List<int> Ids)
        {
            var listcat = Dbcontext.Categories.Where(c=>Ids.Contains(c.Id));
            return listcat.ToList();
        }       
    }
}
