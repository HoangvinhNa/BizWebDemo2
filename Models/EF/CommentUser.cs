namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommentUser")]
    public partial class CommentUser
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string EmailUser { get; set; }

        public string Comment { get; set; }
        public DateTime Send_On { get; set; }
    }
}
