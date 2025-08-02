using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Transport_Fee_Student_Due_2022_20230225
    {
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
        [Unicode(false)]
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
