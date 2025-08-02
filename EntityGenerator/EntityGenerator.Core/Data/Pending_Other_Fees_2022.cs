using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Pending_Other_Fees_2022
    {
        [StringLength(255)]
        public string FeeName { get; set; }
        [StringLength(255)]
        public string School { get; set; }
        public double? AdmissionYear { get; set; }
        public double? AcademicYear { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AdmissionDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [StringLength(255)]
        public string InvoiceNo { get; set; }
        [Column("Adm# No#")]
        [StringLength(255)]
        public string Adm__No_ { get; set; }
        [StringLength(255)]
        public string StudentName { get; set; }
        [StringLength(255)]
        public string ParentName { get; set; }
        [StringLength(255)]
        public string Class { get; set; }
        [StringLength(255)]
        public string Section { get; set; }
        public double? FeeMasterID { get; set; }
        public double? Amount { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
        [StringLength(255)]
        public string F16 { get; set; }
    }
}
