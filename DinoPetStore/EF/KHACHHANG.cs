namespace DinoPetStore.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KHACHHANG")]
    public partial class KHACHHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACHHANG()
        {
            DONDATHANGs = new HashSet<DONDATHANG>();
        }

        [Key]
        public int MAKH { get; set; }

        [Required]
        [StringLength(100)]
        public string HOTENKH { get; set; }

        [Required]
        [StringLength(11)]
        public string DIENTHOAI { get; set; }

        [StringLength(200)]
        public string DIACHI { get; set; }

        [Required]
        [StringLength(30)]
        public string TENDNKH { get; set; }

        [Required]
        [StringLength(200)]
        public string MATKHAUKH { get; set; }

        [StringLength(100)]
        public string EMAIL { get; set; }

        public DateTime? NGAYSINH { get; set; }

        [StringLength(200)]
        public string HINHANH { get; set; }

        [StringLength(200)]
        public string KHOIPHUCMATKHAU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONDATHANG> DONDATHANGs { get; set; }
    }
}
