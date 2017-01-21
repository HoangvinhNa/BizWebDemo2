namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImagePath")]
    public partial class ImagePath
    {
        public int Id { get; set; }

        public string PathImage { get; set; }

        public int ProductId { get; set; }
    }
}
