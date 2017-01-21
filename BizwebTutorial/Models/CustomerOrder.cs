using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizwebTutorial.Models
{
    public class CustomerOrder
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
    }
}