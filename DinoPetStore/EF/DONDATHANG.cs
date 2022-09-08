namespace DinoPetStore.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DONDATHANG")]
    public partial class DONDATHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DONDATHANG()
        {
            CTDONDATHANGs = new HashSet<CTDONDATHANG>();
        }

        [Key]
        public int MADH { get; set; }

        public int MAKH { get; set; }

        public DateTime NGAYDAT { get; set; }

        public DateTime? NGAYGIAO { get; set; }

        public bool? TINHTRANGDH { get; set; }

        public bool? DATHANHTOAN { get; set; }

        public decimal? TONGTIEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTDONDATHANG> CTDONDATHANGs { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}
