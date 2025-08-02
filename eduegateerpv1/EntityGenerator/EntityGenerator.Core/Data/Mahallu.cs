using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Mahallus", Schema = "communities")]
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
        [Column(TypeName = "datetime")]
        public DateTime? EstablishedOn { get; set; }
        [StringLength(50)]
        public string MahalluArea { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(500)]
        public string Logo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CurrentDate { get; set; }
        [StringLength(500)]
        public string Extra1 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExtraDate { get; set; }
    }
}
