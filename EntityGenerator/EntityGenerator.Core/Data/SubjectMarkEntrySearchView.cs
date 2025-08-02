using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SubjectMarkEntrySearchView
    {
        public long? MarkRegisterSubjectMapIID { get; set; }
        public long? ExamID { get; set; }
        [StringLength(100)]
        public string ExamDescription { get; set; }
        [StringLength(50)]
        public string Class { get; set; }
        [StringLength(50)]
        public string Section { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? SubjectID { get; set; }
        [StringLength(500)]
        public string SubjectName { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal? Mark { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
    }
}
