using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Tuition_Fee_20230329
    {
        [StringLength(255)]
        public string Remarks { get; set; }
        [Column("Fee Name")]
        [StringLength(255)]
        public string Fee_Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [StringLength(255)]
        public string InvoiceNo { get; set; }
        [StringLength(255)]
        public string ClassName { get; set; }
        [StringLength(255)]
        public string SectionName { get; set; }
        [StringLength(255)]
        public string AdmissionNumber { get; set; }
        [StringLength(255)]
        public string StudentName { get; set; }
        public double? Amount { get; set; }
    }
}
