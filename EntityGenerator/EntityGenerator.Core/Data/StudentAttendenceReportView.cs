using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentAttendenceReportView
    {
        [StringLength(10)]
        [Unicode(false)]
        public string Expr1 { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(50)]
        public string StatusDescription { get; set; }
        [StringLength(502)]
        public string StudentName { get; set; }
        [StringLength(500)]
        public string Reason { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public long? StudentIID { get; set; }
    }
}
