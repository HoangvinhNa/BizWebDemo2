using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.EF;
using PagedList;
using Models.ProductModelFix;

namespace BizwebTutorial.Dao
{
    public class ProductViewDao
    {
        private BiMartDbContext Dbcontext = new BiMartDbContext();
        public IEnumerable<ProductListTbModel> GetProductByType()
        {
            var ListProductId= Dbcontext.Collections.Where(s=>s.CategoryId== 2101).Select(s=>s.ProductId);
            var listproduct = Dbcontext.Products.Where(c=>ListProductId.Contains(c.Id)).Take(4);
            var listmodel = new List<ProductListTbModel>();
            foreach (var item in listproduct)
            {
                var imageFirst = Dbcontext.ImagePaths.Where(s => s.ProductId == item.Id).FirstOrDefault();
                var productmodel = new ProductListTbModel()
                {
                    Id = item.Id,
                    Price = item.Price,
                    Name = item.Name,
                    Alias = item.Alias,
                    Image = imageFirst != null ? imageFirst.PathImage : ("~/Upload/No_image.png")
                };
                listmodel.Add(productmodel);
            }
            return listmodel;
        }
        public IEnumerable<ProductListTbModel> GetProductOfMachine()
        {
            var ListProductId = Dbcontext.Collections.Where(s => s.CategoryId == 2102).Select(s => s.ProductId);
            var listproduct = Dbcontext.Products.Where(c => ListProductId.Contains(c.Id)).Take(4);
            var listmodel = new List<ProductListTbModel>();
            foreach (var item in listproduct)
            {
                var imageFirst = Dbcontext.ImagePaths.Where(s => s.ProductId == item.Id).FirstOrDefault();
                var productmodel = new ProductListTbModel()
                {
                    Id = item.Id,
                    Price = item.Price,
                    Name = item.Name,
                    Alias = item.Alias,
                    Image = imageFirst != null ? imageFirst.PathImage : ("~/Upload/No_image.png")
                };
                listmodel.Add(productmodel);
            }
            return listmodel;
        }
        public IEnumerable<ProductListTbModel> GetProductOfGift()
        {
            var ListProductId = Dbcontext.Collections.Where(s => s.CategoryId == 1100).Select(s => s.ProductId);
            var listproduct = Dbcontext.Products.Where(c => ListProductId.Contains(c.Id)).Take(4);
            var listmodel = new List<ProductListTbModel>();
            foreach (var item in listproduct)
            {
                var imageFirst = Dbcontext.ImagePaths.Where(s => s.ProductId == item.Id).FirstOrDefault();
                var productmodel = new ProductListTbModel()
                {
                    Id = item.Id,
                    Price = item.Price,
                    Name = item.Name,
                    Alias = item.Alias,
                    Image = imageFirst != null ? imageFirst.PathImage : ("~/Upload/No_image.png")
                };
                listmodel.Add(productmodel);
            }
            return listmodel;
        }
        public IEnumerable<ProductListTbModel> GetAllProductToView(string sortname, string searchstring, string curentfillter, int? page)
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
                    Price = item.Price,
                    Name = item.Name,
                    Alias = item.Alias,
                    Image = imageFirst != null ? imageFirst.PathImage : ("~/Upload/No_image.png")
                };
                listmodel.Add(productmodel);
            }
            var listmodel1 = listmodel.AsEnumerable();
            switch (sortname)
            {
                case "AZ":
                    listmodel1 = listmodel1.OrderBy(s => s.Name);//
                    break;
                case "ZA":
                    listmodel1 = listmodel1.OrderByDescending(s => s.Name);//z-a
                    break;
                case "priceUp":
                    listmodel1 = listmodel1.OrderBy(s => s.Price);
                    break;
                case "priceDown":
                    listmodel1 = listmodel1.OrderByDescending(s => s.Price);
                    break;
                case "IdUp":
                    listmodel1 = listmodel1.OrderBy(s => s.Id);
                    break;
                case "IdDown":
                    listmodel1 = listmodel1.OrderByDescending(s => s.Id);//max-min
                    break;
                default:
                    listmodel1 = listmodel1.OrderBy(s => s.Name);//a-z
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
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return listmodel1.ToPagedList(pageNumber, pageSize);
        }

        public ProductDeatailModel getproductbyAlias(string alias)
        {
            var product = Dbcontext.Products.Where(s => s.Alias == alias).SingleOrDefault();
            var productId = product.Id;
            var imagelist = Dbcontext.ImagePaths.Where(s => s.ProductId == productId).ToList();
            var listimageobjects = new List<ImageDetailsProduct>();
            var listimageobjectsnull = new List<ImageDetailsProduct>();
            if (imagelist != null && imagelist.Count() > 0)
            {
                foreach (var item in imagelist)
                {
                    if (item == null) continue;
                    var imagemodel = new ImageDetailsProduct()
                    {
                        PathImage = item.PathImage,
                        ProductId = item.ProductId,
                        Id = item.Id
                    };
                    listimageobjects.Add(imagemodel);
                }
            }
            var imagemodel2 = new ImageDetailsProduct()
            {
                PathImage = "~/Upload/No_image.png"
            };
            listimageobjectsnull.Add(imagemodel2);
            var productmodel = new ProductDeatailModel()
            {
                Id = product.Id,
                Alias = product.Alias,
                Barcode = product.Barcode,
                Descripstion = product.Descripstion,
                MetaDescription = product.MetaDescription,
                ShortDescription=product.ShortDescription,
                MetaTitle=product.MetaTitle,
                OldPrice=product.OldPrice,
                Name = product.Name,
                Price = product.Price,
                Sku = product.Sku,
                Tag = product.Tag,
                ListImage = listimageobjects.Count() > 0 ? listimageobjects : listimageobjectsnull
            };
            return productmodel;
        }
        public ProductDeatailModel getDetailProduct(int Id)
        {
            var product = Dbcontext.Products.Find(Id);
            var imagelist = Dbcontext.ImagePaths.Where(s => s.ProductId == Id).ToList();
            var listimageobjects = new List<ImageDetailsProduct>();
            var listimageobjectsnull = new List<ImageDetailsProduct>();
            if (imagelist != null && imagelist.Count() > 0)
            {
                foreach (var item in imagelist)
                {
                    if (item == null) continue;
                    var imagemodel = new ImageDetailsProduct()
                    {
                        PathImage = item.PathImage,
                        ProductId = item.ProductId,
                        Id = item.Id
                    };
                    listimageobjects.Add(imagemodel);
                }
            }
            var imagemodel2 = new ImageDetailsProduct()
            {
                PathImage = "~/Upload/No_image.png"
            };
            listimageobjectsnull.Add(imagemodel2);
            var productmodel = new ProductDeatailModel()
            {
                Id = product.Id,
                Alias = product.Alias,
                Barcode = product.Barcode,
                Descripstion = product.Descripstion,
                MetaDescription = product.MetaDescription,
                Name = product.Name,
                Price = product.Price,
                Sku = product.Sku,
                Tag = product.Tag,
                ListImage = listimageobjects.Count() > 0 ? listimageobjects : listimageobjectsnull
            };
            return productmodel;
        }
    }
}