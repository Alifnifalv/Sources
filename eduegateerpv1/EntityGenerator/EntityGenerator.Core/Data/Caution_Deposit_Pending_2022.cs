using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Caution_Deposit_Pending_2022
    {
        [StringLength(255)]
        public string SchoolName { get; set; }
        public double? AdmissionYear { get; set; }
        public double? AcademicYear { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AdmissionDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [StringLength(255)]
        public string InvoiceNo { get; set; }
        [StringLength(255)]
        public string AdmissionNumber { get; set; }
        [StringLength(255)]
        public string StudentName { get; set; }
        [StringLength(255)]
        public string ParentName { get; set; }
        [StringLength(255)]
        public string ClassName { get; set; }
        [StringLength(255)]
        public string SectionName { get; set; }
        public double? Amount { get; set; }
        [StringLength(255)]
        public string Status { get; set; }
    }
}
