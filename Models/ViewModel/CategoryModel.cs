using PagedList;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Models.ViewModel
{
    public class CategoryModel
    {
        [Required(ErrorMessage ="Nhập vào tên danh mục")]
        [StringLength(20,ErrorMessage ="Tên Danh Mục Không Quá 20 Kí tự")]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }
        public string Image { get; set; }
        public string Alias { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string MetaTittle { get; set; }
        public string MetaDiscription { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsVisible { get; set; }
    }
}
