namespace DinoPetStore.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LIENHE")]
    public partial class LIENHE
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        public string ContentContact { get; set; }

        [Required]
        [StringLength(200)]
        public string RequestBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime RequestDate { get; set; }

        public bool IsResponse { get; set; }
    }
}
