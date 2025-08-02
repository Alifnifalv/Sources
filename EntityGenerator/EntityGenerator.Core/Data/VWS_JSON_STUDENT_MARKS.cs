using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VWS_JSON_STUDENT_MARKS
    {
        public long StudentIID { get; set; }
        [StringLength(20)]
        public string AcademicYearCode { get; set; }
        [StringLength(50)]
        public string ClassName { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        public string StudentMarks { get; set; }
    }
}
