using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
using System.Web.Mvc;

namespace Models.ViewModel
{
    public class CustomerEditGetOrderModel
    {
        public CustomerEditGetOrderModel()
        {
            ListOrders = new List<OrderMapingCustomer>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public bool Marketing { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        [AllowHtml]
        public string Note { get; set; }
        public string Tag { get; set; }
        public string City { get; set; }
        public string Township { get; set; }
        public DateTime ModifiedOn { get; set; }
        public List<OrderMapingCustomer> ListOrders { get; set; }
    }
    public class OrderMapingCustomer
    {
        public int Idcustomer { get; set; }
        public int IdOrder { get; set; }
        public string NameOrder { get; set; }
        public long TotalMoney { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
