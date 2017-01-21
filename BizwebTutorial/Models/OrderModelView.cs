using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BizwebTutorial.Models
{
    public class OrderModelView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long TotalMoney { get; set; }
        public string LineItemString { get; set; }
        public bool Payment { get; set; }
        public List<CartViewModel> ListItemProduct { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Nhập vào Email Khách Hàng")]
        [StringLength(30)]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng định dạng")]
        public string EmailCustomer { get; set; }
        [Required(ErrorMessage = "Nhập vào Tên Khách Hàng")]
        [StringLength(30)]
        public string NameCustomer { get; set; }
        [Required(ErrorMessage = "Nhập vào địa chỉ khách hàng")]
        [StringLength(30)]
        public string AddressCustomer { get; set; }
        [Required(ErrorMessage = "Nhập vào số điện thoại ")]
        [StringLength(30)]
        public string PhoneCustomer { get; set; }
    }
}