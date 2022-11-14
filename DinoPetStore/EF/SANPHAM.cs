namespace DinoPetStore.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SANPHAM")]
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            CTDONDATHANGs = new HashSet<CTDONDATHANG>();
            GIAMGIAs = new HashSet<GIAMGIA>();
            HINHs = new HashSet<HINH>();
            KICHTHUOCs = new HashSet<KICHTHUOC>();
            PHIEUNHAPKHOes = new HashSet<PHIEUNHAPKHO>();
        }

        [Key]
        public int MASP { get; set; }

        [Required]
        [StringLength(200)]
        public string TENSP { get; set; }

        public decimal? DONGIAMUA { get; set; }

        public decimal? DONGIABAN { get; set; }

        public int MATH { get; set; }

        public int MALOAI { get; set; }

        public int? MAMAUSAC { get; set; }

        public int? SOLUONG { get; set; }

        [StringLength(100)]
        public string HINHANH { get; set; }

        [Required]
        public string MOTA { get; set; }

        [StringLength(200)]
        public string THANHTOANON { get; set; }

        public bool? ANHIEN { get; set; }

        public DateTime NGAYTAO { get; set; }

        public int? CategoryId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTDONDATHANG> CTDONDATHANGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GIAMGIA> GIAMGIAs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HINH> HINHs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KICHTHUOC> KICHTHUOCs { get; set; }

        public virtual LOAI LOAI { get; set; }

        public virtual MAUSAC MAUSAC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUNHAPKHO> PHIEUNHAPKHOes { get; set; }

        public virtual THUONGHIEU THUONGHIEU { get; set; }
    }
}
