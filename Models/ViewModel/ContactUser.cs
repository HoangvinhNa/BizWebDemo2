using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class ContactUser
    {
        [Required]
        [StringLength(50,ErrorMessage ="Email chỉ dưới 50 ký tự")]
        [EmailAddress(ErrorMessage ="Sai định dạng Email")]
        public string EmailUser { get; set; }
        [Required]
        public string Comment { get; set; }
        public DateTime Send_On { get; set; }
    }
}
