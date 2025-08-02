using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CosColasticReportRemarksView
    {
        public long? StudentID { get; set; }
        [StringLength(502)]
        public string StudentName { get; set; }
        public long RemarksEntryIID { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(115)]
        public string AcademicYearName { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public int? ExamGroupID { get; set; }
        [StringLength(100)]
        public string ExamGroupName { get; set; }
        public int? SectionID { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
    }
}
