namespace DinoPetStore.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GIAMGIA")]
    public partial class GIAMGIA
    {
        [Key]
        public int MAGIAMGIA { get; set; }

        public int MASP { get; set; }

        public int PHAMTRAMGIAM { get; set; }
        [Required]
        public DateTime TUNGAY { get; set; }
        [Required]
        public DateTime DENNGAY { get; set; }

        public bool? ANHIEN { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
