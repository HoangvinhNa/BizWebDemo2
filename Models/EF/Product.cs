namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Descripstion { get; set; }

        [StringLength(255)]
        public string ProductType { get; set; }

        [StringLength(255)]
        public string Vendor { get; set; }

        public string Image { get; set; }

        public long Price { get; set; }

        public long? OldPrice { get; set; }

        public bool? Vat { get; set; }

        [StringLength(50)]
        public string Sku { get; set; }

        [StringLength(255)]
        public string Barcode { get; set; }

        public bool? Store { get; set; }

        public int? Quantity { get; set; }

        public bool? IsOrder { get; set; }

        [StringLength(50)]
        public string Tag { get; set; }

        [Required]
        [StringLength(255)]
        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        [Required]
        [StringLength(255)]
        public string Alias { get; set; }

        public bool IsVisible { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ShortDescription { get; set; }
    }
}
