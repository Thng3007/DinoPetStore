namespace DinoPetStore.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHANQUYEN")]
    public partial class PHANQUYEN
    {
        [Key]
        [Column(Order = 0)]

        public int MAPQ { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MAADMIN { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string MACHUCNANG { get; set; }

        public virtual ADMIN ADMIN { get; set; }

        public virtual CHUCNANG_QUYEN CHUCNANG_QUYEN { get; set; }
    }
}
