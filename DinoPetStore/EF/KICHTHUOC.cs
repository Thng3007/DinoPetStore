namespace DinoPetStore.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KICHTHUOC")]
    public partial class KICHTHUOC
    {
        [Key]
        [Column(Order = 0)]
        public int MAKICHTHUOC { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int MASP { get; set; }

        public int TENKICHTHUOC { get; set; }

        public bool? ANHIEN { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
