using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Models.ViewModel
{
    public class CategoryEditModel
    {
        public CategoryEditModel()
        {
            ListProduct = new List<ProductMappingModel>();
        }
        public List<ProductMappingModel> ListProduct { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "Nhập vào tên danh mục")]
        [StringLength(20,ErrorMessage ="Tên Danh Mục Không Quá 20 kí tự")]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }
        public string Alias { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string MetaTittle { get; set; }
        public string MetaDiscription { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsVisible { get; set; }
        public string Image { get; set; }
    }
    public class ProductMappingModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string ProductImage { get; set; }
        public string CategoryImage { get; set; }
    }
}
