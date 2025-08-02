using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ClassWiseStudentListReportView
    {
        public long StudentIID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public byte? GenderID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [StringLength(502)]
        public string StudentName { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(50)]
        public string Gender { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MobileNumber { get; set; }
        [StringLength(50)]
        public string EmailID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
    }
}
