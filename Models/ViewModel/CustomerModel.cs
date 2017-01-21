
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Models.ViewModel
{
    public class CustomerModel
    {
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Nhập vào Tên Khách Hàng")]
        [StringLength(20, ErrorMessage = "Tên Không Quá 20 kí tự")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Nhập vào Email Khách Hàng")]
        [StringLength(30)]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng định dạng")]
        public string Email { get; set; }
        public bool Marketing { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Township { get; set;}
        [AllowHtml]
        public string Note { get; set; }
        public string Tag { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
