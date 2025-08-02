namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("communities.Mahallus")]
    public partial class Mahallu
    {
        [Key]
        public long MahalluIID { get; set; }

        [StringLength(500)]
        public string MahalluName { get; set; }

        [StringLength(50)]
        public string Place { get; set; }

        [StringLength(50)]
        public string Post { get; set; }

        [StringLength(50)]
        public string Pincode { get; set; }

        [StringLength(50)]
        public string District { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        [StringLength(50)]
        public string WaqafNumber { get; set; }

        public DateTime? EstablishedOn { get; set; }

        [StringLength(50)]
        public string MahalluArea { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(500)]
        public string Logo { get; set; }

        public DateTime? CurrentDate { get; set; }

        [StringLength(500)]
        public string Extra1 { get; set; }

        public DateTime? ExtraDate { get; set; }
    }
}
