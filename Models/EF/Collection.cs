namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Collection
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int ProductId { get; set; }
    }
}
