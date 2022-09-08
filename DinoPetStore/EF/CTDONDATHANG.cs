namespace DinoPetStore.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTDONDATHANG")]
    public partial class CTDONDATHANG
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MADH { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MASP { get; set; }

        public int SOLUONG { get; set; }

        public int DONGIA { get; set; }

        public decimal? THANHTIEN { get; set; }

        public virtual DONDATHANG DONDATHANG { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
