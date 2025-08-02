using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentApplication_VacanyLetter_Report
    {
        public long ApplicationIID { get; set; }
        [Required]
        [StringLength(152)]
        public string StudentName { get; set; }
        [StringLength(50)]
        public string SchoolCode { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [StringLength(4000)]
        public string ClassName { get; set; }
        [StringLength(4000)]
        public string AcademicYear { get; set; }
        public int? SchoolAcademicyearID { get; set; }
        public byte? SchoolID { get; set; }
        public int? ClassID { get; set; }
        [StringLength(25)]
        public string StudentNationalID { get; set; }
    }
}
