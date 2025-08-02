using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FineMasterStudentMapSearch
    {
        public long FineMasterStudentMapIID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public int? FineMasterID { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? SchoolID { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [Required]
        [StringLength(13)]
        [Unicode(false)]
        public string IsCollected { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        public long? StudentId { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FineMapDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(502)]
        public string StudentName { get; set; }
        [StringLength(100)]
        public string FineMasterName { get; set; }
    }
}
