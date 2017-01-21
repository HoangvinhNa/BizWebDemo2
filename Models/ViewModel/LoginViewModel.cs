using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class LoginViewModelUser
    {
        [Required(ErrorMessage = "Mời Nhập Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mời Nhập Password")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
