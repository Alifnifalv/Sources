using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FEE_TUITION_OUTSTANDING_2023
    {
        public long? StudentFeeDueIID { get; set; }
        public int? TermID { get; set; }
        [StringLength(72)]
        [Unicode(false)]
        public string TermName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? TermStart { get; set; }
        [Column(TypeName = "date")]
        public DateTime? TermEnd { get; set; }
        public int? EntryType { get; set; }
        public long? RowIndex { get; set; }
        public int? StudentIID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string AdmissionNumber { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string StudentName { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string ClassName { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string SectionName { get; set; }
        public int? SchoolID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string SchoolName { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string FatherName { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string FatherMobile { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string GuardianPhone { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string MotherName { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string MotherMobile { get; set; }
        public int? FeeMasterID { get; set; }
        public int FeePeriodID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DocDate { get; set; }
        [StringLength(100)]
        public string DocNo { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string DocRef { get; set; }
        public int PartType { get; set; }
        [Required]
        [StringLength(15)]
        [Unicode(false)]
        public string Particulars { get; set; }
        [StringLength(2000)]
        public string Ref { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Amount { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string FeePeriod { get; set; }
        public int? FeeType { get; set; }
        [Required]
        [StringLength(13)]
        [Unicode(false)]
        public string FeeTypeName { get; set; }
        [StringLength(203)]
        [Unicode(false)]
        public string FeeName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string InvoiceNo { get; set; }
        public int? FeeCycleID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Cycle { get; set; }
        public int InPeriod { get; set; }
    }
}
