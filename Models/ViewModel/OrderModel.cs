using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Models.ViewModel
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalMoney { get; set; }
        public string LineItems { get; set; }
        [AllowHtml]
        public string Note { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAdress{ get; set; }
        public bool Payment { get; set; }
        public DateTime CreatedOn { get; set; }
    }
    public class LineItemModel
    {
        public int Id { get; set; }
        public string NameProduct { get; set; }
        public string ImageProduct { get; set; }
        public long PriceProduct { get; set; }
        public int QuantityProduct { get; set; }
    }
    public class OrderEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long TotalMoney { get; set; }
        public string LineItems { get; set; }
        [AllowHtml]
        public string Note { get; set; }
        public int CustomerId { get; set; }
        public string CustomerAdress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public bool Payment { get; set; }
        public bool Transport { get; set; }
        public DateTime ModifiedOn { get; set; }
        public List<LineItemModel> Items { get; set; }
    }
}
    
    

