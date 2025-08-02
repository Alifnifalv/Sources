using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Pending_CautionDeposit_2023
    {
        public double? StudentId { get; set; }
        public double? StudentFeeDueID { get; set; }
        public double? FeeDueFeeTypeMapsID { get; set; }
        [StringLength(255)]
        public string SchoolName { get; set; }
        [StringLength(255)]
        public string ClassName { get; set; }
        [StringLength(255)]
        public string SectionName { get; set; }
        [StringLength(255)]
        public string AdmissionNumber { get; set; }
        [StringLength(255)]
        public string StudentName { get; set; }
        [StringLength(255)]
        public string ParentCode { get; set; }
        [StringLength(255)]
        public string ParentName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [StringLength(255)]
        public string InvoiceNo { get; set; }
        public double? Caution_Deposit { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
    }
}
