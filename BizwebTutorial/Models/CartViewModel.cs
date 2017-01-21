using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizwebTutorial.Models
{
    public class ListCartModel
    {
        public ListCartModel()
        {
            ListCarts = new List<CartViewModel>();
        }
        public List<CartViewModel> ListCarts { get; set; }
        public string LineItems { get; set; }
    }
    public class CartViewModel
    {
        public int Id { get; set; }
        public string NameProduct { get; set; }
        public string ImageProduct { get; set; }
        public long PriceProduct { get; set; }
        public int QuantityProduct { get; set; }
    }
}