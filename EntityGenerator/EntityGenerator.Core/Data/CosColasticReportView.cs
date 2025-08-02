using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CosColasticReportView
    {
        public long? StudentIID { get; set; }
        [StringLength(502)]
        public string StudentName { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public int? SectionID { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(100)]
        public string SkillMasterName { get; set; }
        [StringLength(100)]
        public string SkillGroup { get; set; }
        [StringLength(100)]
        public string ExamDescription { get; set; }
        [StringLength(100)]
        public string AssessmentTerm { get; set; }
        [StringLength(50)]
        public string GradeName { get; set; }
        [StringLength(50)]
        public string GroupSubjectName { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(115)]
        public string AcademicYearName { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
    }
}
