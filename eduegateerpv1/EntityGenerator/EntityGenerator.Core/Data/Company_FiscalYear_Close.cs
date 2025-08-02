using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Company_FiscalYear_Close", Schema = "account")]
    public partial class Company_FiscalYear_Close
    {
        [Key]
        public int CFC_ID { get; set; }
        public int FiscalYear_ID { get; set; }
        public int CompanyID { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string CFC_Name { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? CFC_StartDate { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? CFC_EndDate { get; set; }
        public int CFC_Type { get; set; }
        public bool IsActive { get; set; }
        public int? DeActiveBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? DeActiveDate { get; set; }
        public bool IsHide { get; set; }
        public int? HideBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? HideDate { get; set; }
        public int TempClosedBy { get; set; }
        public bool IsTempClosed { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? TempClosedDate { get; set; }
        public int AuditedBy { get; set; }
        public bool IsAudited { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? AuditedDate { get; set; }
        public int ClosedBy { get; set; }
        public bool IsClosed { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? ClosedDate { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }
        public int LastModifiedBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? LastModifiedDate { get; set; }
        [Column(TypeName = "money")]
        public decimal OpeningStock { get; set; }
        [Column(TypeName = "money")]
        public decimal ClosingStock { get; set; }
        [Column(TypeName = "money")]
        public decimal GrossProfit { get; set; }
        [Column(TypeName = "money")]
        public decimal NetProfit { get; set; }
        [Column(TypeName = "money")]
        public decimal Diff_In_Opening { get; set; }
    }
}
