using System;

namespace Models.ProductModelFix
{
    public class ProductListTbModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductType { get; set; }
        public string Vendor { get; set; }
        public string Image { get; set; }
        public long Price { get; set; }
        public string Alias { get; set; }
    }
}
