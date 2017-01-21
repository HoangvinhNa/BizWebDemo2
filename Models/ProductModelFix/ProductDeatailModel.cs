using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ProductModelFix
{
    public class ProductDeatailModel
    {
        public ProductDeatailModel()
        {
            ListImage = new List<ImageDetailsProduct>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descripstion { get; set; }
        public string ShortDescription { get; set; }
        public long Price { get; set; }
        public long? OldPrice { get; set; }
        public string Sku { get; set; }
        public string Barcode { get; set; }
        public string Tag { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Alias { get; set; }
        public List<ImageDetailsProduct> ListImage { get; set; }
    }
    public class ImageDetailsProduct
    {
        public int Id { get; set; }

        public string PathImage { get; set; }

        public int ProductId { get; set; }
    }
}
