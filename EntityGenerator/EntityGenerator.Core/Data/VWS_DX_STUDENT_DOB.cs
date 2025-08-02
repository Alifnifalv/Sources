using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VWS_DX_STUDENT_DOB
    {
        [StringLength(8000)]
        [Unicode(false)]
        public string MobileNumber { get; set; }
        [StringLength(50)]
        public string EmailID { get; set; }
        [Required]
        [StringLength(500)]
        public string StudentProfile { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public long StudentIID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [StringLength(50)]
        public string ClassName { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        public int? Age { get; set; }
        [Required]
        [StringLength(1500)]
        public string ImageLink { get; set; }
        [StringLength(104)]
        public string ClassSection { get; set; }
    }
}
