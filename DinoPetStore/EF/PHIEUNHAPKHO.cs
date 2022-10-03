namespace DinoPetStore.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEUNHAPKHO")]
    public partial class PHIEUNHAPKHO
    {
        [Key]
        public int MAPHIEUNK { get; set; }

        public DateTime NGAYNK { get; set; }

        public int MASP { get; set; }

        public int SOLUONG { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
