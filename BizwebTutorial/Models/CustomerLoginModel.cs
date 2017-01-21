using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BizwebTutorial.Models
{
    public class CustomerLoginModel
    {
        public int ID { get; set;}
        [Required(ErrorMessage ="Email không để trống")]
        [EmailAddress(ErrorMessage ="Email không đúng đinh dạng")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Mật khẩu không được để trống")]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}