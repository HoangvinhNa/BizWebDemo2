using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.EF;
namespace BizwebTutorial.Models
{
    public class CategoryViewModel :Category
    {
        public List<ProductMaptoCategory> ListProducts { get; set; }
        public CategoryViewModel()
        {
            ListProducts = new List<ProductMaptoCategory>();
        }
    }
    public class ProductMaptoCategory
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string ProductAlias { get; set; }
        public string ProductImage { get; set; }
        public long Productprice { get; set; }
        public long? OldPrice { get; set; }
    }
}