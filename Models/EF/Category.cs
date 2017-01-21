namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Alias { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string MetaTittle { get; set; }

        [StringLength(255)]
        public string MetaDiscription { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsVisible { get; set; }

        public string Image { get; set; }
    }
}
