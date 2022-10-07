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
        public int MAPQ { get; set; }

        public int MAADMIN { get; set; }

        [Required]
        [StringLength(100)]
        public string MACHUCNANG { get; set; }

        public virtual ADMIN ADMIN { get; set; }

        public virtual CHUCNANG_QUYEN CHUCNANG_QUYEN { get; set; }
    }
}
