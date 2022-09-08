namespace DinoPetStore.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HINH")]
    public partial class HINH
    {
        [Key]
        [Column(Order = 0)]
        public int MAHINH { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MASP { get; set; }

        [StringLength(100)]
        public string HINH1 { get; set; }

        public bool? ANHIEN { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
