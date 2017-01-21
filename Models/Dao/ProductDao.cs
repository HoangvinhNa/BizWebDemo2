using System;
using System.Collections.Generic;
using System.Linq;
using Models.EF;
using Models.ViewModel;
using PagedList;
using System.Web;
using Models.ProductModelFix;

namespace Models.Dao
{
    public class ProductDao
    {
        private BiMartDbContext DbContext = new BiMartDbContext();

        public IEnumerable<ProductListTbModel> GetallProduct(string sortname, string searchstring, string curentfillter, int? page)
        {
            var product = from s in DbContext.Products
                          select s;
            var listmodel = new List<ProductListTbModel>();
            foreach (var item in product)
            {
                var imageFirst = DbContext.ImagePaths.Where(s => s.ProductId == item.Id).FirstOrDefault();
                var productmodel = new ProductListTbModel()
                {
                    Id = item.Id,
                    Vendor = item.Vendor,
                    ProductType = item.ProductType,
                    Name = item.Name,
                    Image = imageFirst != null ? imageFirst.PathImage : ("~/Upload/No_image.png")
                };
                listmodel.Add(productmodel);
            }
            var listmodel1 = listmodel.AsQueryable();
            switch (sortname)
            {
                case "NameSort":
                    listmodel1 = listmodel1.OrderByDescending(s => s.Name);
                    break;
                default:
                    listmodel1 = listmodel1.OrderByDescending(s => s.Id);
                    break;
            }
            if (!string.IsNullOrEmpty(searchstring))
            {
                listmodel1 = listmodel1.Where(s => s.Name.Contains(searchstring));
            }
            if (searchstring != null)
            {
                page = 1;
            }
            else
            {
                searchstring = curentfillter;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return listmodel1.ToPagedList(pageNumber, pageSize);
        }

        public int Insert(ProductModel entity)
        {
            var product = new Product()
            {
                Name = entity.Name,
                Descripstion = entity.Descripstion,
                ProductType = entity.ProductType,
                Vendor = entity.Vendor,
                Image = entity.Image,
                Price = entity.Price,
                ShortDescription = entity.ShortDescription,
                OldPrice = entity.OldPrice,
                Sku = entity.Sku,
                Barcode = entity.Barcode,
                MetaTitle = entity.MetaTitle,
                MetaDescription = entity.MetaDescription,
                Alias = entity.Alias,
                CreatedOn = DateTime.Now,
                IsVisible = entity.IsVisible
            };
            var checkalias = DbContext.Products.Any(s => s.Alias == entity.Alias);
            if (checkalias)
            {
                return 0;
            }
            else
            {
                DbContext.Products.Add(product);
                DbContext.SaveChanges();
                return product.Id;
            }
        }

        public Product FindById(int id)
        {
            return DbContext.Products.Find(id);
        }
        public bool Update(ProductModel entity)
        {
            try
            {
                var product = DbContext.Products.Find(entity.Id);
                var ListAlias = DbContext.Products.Select(m => m.Alias).ToList();
                var Aliasentity = DbContext.Products.Find(entity.Id).Alias.ToString();
                ListAlias.Remove(Aliasentity);
                var Lisst12 = ListAlias.AsEnumerable();
                var checkalias = Lisst12.Any(item => item == entity.Alias);
                product.Name = entity.Name;
                product.Descripstion = entity.Descripstion;
                product.ProductType = entity.ProductType;
                product.Vendor = entity.Vendor;
                product.Price = entity.Price;
                product.OldPrice = entity.OldPrice;
                product.Sku = entity.Sku;
                product.ShortDescription = entity.ShortDescription;
                product.Barcode = entity.Barcode;
                product.MetaTitle = entity.MetaTitle;
                product.MetaDescription = entity.MetaDescription;
                product.Alias = entity.Alias;
                product.IsVisible = entity.IsVisible;
                product.ModifiedOn = DateTime.Now;
                if (checkalias==true)
                {
                    return false;
                }
                else
                {
                    DbContext.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool Insertimage(int id, string image)
        {
            var product = DbContext.Products.Find(id);
            product.Image = image;
            DbContext.SaveChanges();
            return true;
        }
        public bool DeleteByID(int id)
        {
            Product product = DbContext.Products.Find(id);
            DbContext.Products.Remove(product);
            DbContext.SaveChanges();
            return true;
        }

        public IEnumerable<Product> GetListProductIncollect(List<int> ids)
        {
            var list = DbContext.Products.Where(c => ids.Contains(c.Id));
            return list.ToList();
        }
        public IEnumerable<Category> GetminiCategory(string sortname, string searchstring, string curentfillter, int? page)
        {
            var category = from s in DbContext.Categories
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
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return category.ToPagedList(pageNumber, pageSize);
        }
    }
}
