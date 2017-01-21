namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        public long TotalMoney { get; set; }

        [Required]
        public string LineItems { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        [StringLength(255)]
        public string Tag { get; set; }

        public bool Payment { get; set; }

        public bool Transport { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [StringLength(250)]
        public string CustomerAddress { get; set; }

        [StringLength(50)]
        public string CustomerPhone { get; set; }

        [StringLength(250)]
        public string CustomerEmail { get; set; }

        [StringLength(50)]
        public string CustomerName { get; set; }

        public int CustomerId { get; set; }
    }
}
