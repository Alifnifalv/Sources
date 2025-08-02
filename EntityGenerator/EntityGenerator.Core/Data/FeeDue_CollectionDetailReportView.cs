using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeDue_CollectionDetailReportView
    {
        [StringLength(20)]
        public string AcademicYearCode { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [StringLength(50)]
        public string FeePeriodDes { get; set; }
        public byte FeeCycleID { get; set; }
        [StringLength(50)]
        public string Cycle { get; set; }
        public int FeeMasterID { get; set; }
        [StringLength(50)]
        public string FeeDescription { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FeePeriodFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FeePeriodTo { get; set; }
        public int? Yr { get; set; }
        public int? Mn { get; set; }
        [StringLength(34)]
        [Unicode(false)]
        public string YrMn { get; set; }
        [StringLength(30)]
        public string MnName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [StringLength(502)]
        public string StudentName { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? DueAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal CollectedAmount { get; set; }
        [StringLength(50)]
        public string InvoiceNo { get; set; }
    }
}
