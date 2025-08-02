using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SalesVoucherSearchView
    {
        public long HeadIID { get; set; }
        public int? DocumentTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string DespatchID { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string CustomerCode { get; set; }
        [StringLength(81)]
        public string Customer { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
        [StringLength(50)]
        public string LeadCode { get; set; }
        [StringLength(100)]
        public string LeadName { get; set; }
        public long? ReportingEmployeeID { get; set; }
        [StringLength(500)]
        public string Reference { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Rate { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? TaxAmount { get; set; }
    }
}
