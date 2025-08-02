using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Student_Class_History_Report_View
    {
        [StringLength(2001)]
        [Unicode(false)]
        public string FROMACADEMICYEAR { get; set; }
        [StringLength(2001)]
        [Unicode(false)]
        public string TOACADEMICYEAR { get; set; }
        public int ClassID { get; set; }
        [StringLength(50)]
        public string FromClass { get; set; }
        [Required]
        [StringLength(50)]
        public string FromSection { get; set; }
        [Required]
        [StringLength(50)]
        public string ToSection { get; set; }
        [StringLength(50)]
        public string ToClass { get; set; }
        public bool Status { get; set; }
        public string Remarks { get; set; }
        public byte? SchoolID { get; set; }
        public long StudentID { get; set; }
    }
}
