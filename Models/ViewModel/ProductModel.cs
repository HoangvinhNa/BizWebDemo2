using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Models.ViewModel
{
    public class ProductModel
    {
        public ProductModel()
        {
            ListCategory = new List<CategoryMappingModel>();
            ListImagePath = new List<ImagePathMapingProduct>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage ="Nhập vào tên sản phẩm")]
        [StringLength(20)]
        public string Name { get; set; }
        [AllowHtml]
        public string Descripstion { get; set; }
        [AllowHtml]
        public string ShortDescription { get; set; }
        public string ProductType { get; set; }

        public string Vendor { get; set; }

        public string Image { get; set; }
       
        public long Price { get; set; }

        public long? OldPrice { get; set; }

        public string Sku { get; set; }

        public string Barcode { get; set; }

        public string MetaTitle { get; set; }
       
        public string MetaDescription { get; set; }

        public string Alias { get; set; }

        public bool IsVisible { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CategoryIdsString { get; set; }
        public List<CategoryMappingModel> ListCategory { get; set; }
        public List<ImagePathMapingProduct> ListImagePath { get; set; }
    }

   public class ImagePathMapingProduct
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public int ProductId { get; set; }
    }
    public class CategoryMappingModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string ProductImage { get; set; }
        public string CategoryImage { get; set; }
    }
}
