using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LibraryBookDetailsSearch
    {
        public long? LibraryBookIID { get; set; }
        [StringLength(100)]
        public string Call_No { get; set; }
        [StringLength(203)]
        public string Acc_No { get; set; }
        [StringLength(100)]
        public string BookTitle { get; set; }
        [StringLength(50)]
        public string LibraryBookType { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(50)]
        public string BookNumber { get; set; }
        public string ISBNNumber { get; set; }
        [StringLength(50)]
        public string Subject { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? BookPrice { get; set; }
        public string Publisher { get; set; }
        [StringLength(20)]
        public string RackNumber { get; set; }
        [StringLength(100)]
        public string Author { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? Quatity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PostPrice { get; set; }
        [StringLength(200)]
        public string BookPhoto { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        [StringLength(100)]
        public string Pages { get; set; }
        [StringLength(100)]
        public string Year { get; set; }
        [StringLength(100)]
        public string Bill_No { get; set; }
        [StringLength(200)]
        public string PlaceOfPublication { get; set; }
    }
}
