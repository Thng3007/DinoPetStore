namespace DinoPetStore.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ADMIN")]
    public partial class ADMIN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMIN()
        {
            PHANQUYENs = new HashSet<PHANQUYEN>();
        }

        [Key]
        public int MAADMIN { get; set; }

        [Required]
        [StringLength(100)]
        public string HOTEN { get; set; }

        [Required]
        [StringLength(200)]
        public string DIACHI { get; set; }

        [Required]
        [StringLength(11)]
        public string DIENTHOAI { get; set; }

        [Required]
        [StringLength(100)]
        public string TENLOAI { get; set; }

        [Required]
        [StringLength(30)]
        public string TENDN { get; set; }

        [Required]
        [StringLength(200)]
        public string MATKHAU { get; set; }

        [Required]
        [StringLength(100)]
        public string AVATAR { get; set; }

        [Required]
        [StringLength(50)]
        public string EMAIL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHANQUYEN> PHANQUYENs { get; set; }
    }
}
