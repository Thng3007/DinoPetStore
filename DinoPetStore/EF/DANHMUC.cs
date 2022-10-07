namespace DinoPetStore.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DANHMUC")]
    public partial class DANHMUC
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(500)]
        public string CategoryName { get; set; }
    }
}
