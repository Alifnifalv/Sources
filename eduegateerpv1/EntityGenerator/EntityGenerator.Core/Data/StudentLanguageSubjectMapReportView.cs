using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentLanguageSubjectMapReportView
    {
        public long StudentIID { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public int? ClassID { get; set; }
        [StringLength(4000)]
        public string ClassName { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        public int? SectionID { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(50)]
        public string Gender { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(100)]
        public string AcademicYear { get; set; }
        public int? SecondLangID { get; set; }
        [StringLength(500)]
        public string SecondLanguage { get; set; }
        public int? ThirdLangID { get; set; }
        [StringLength(500)]
        public string Thirdlanguage { get; set; }
        public int? MoralOrIslamicID { get; set; }
        [StringLength(500)]
        public string MoralOrIslamic { get; set; }
    }
}
