using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StockCompareHead", Schema = "inventory")]
    public partial class StockCompareHead
    {
        [Key]
        public long HeadIID { get; set; }
        public int? CompanyID { get; set; }
        public long? BranchID { get; set; }
        public int? DocumentTypeID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        public byte? TransactionStatusID { get; set; }
        public long? ReferenceHeadID { get; set; }
        public int? CurrencyID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ExchangeRate { get; set; }
        public long? EmployeeID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(50)]
        public string Reference { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DocumentCancelledDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ExternalReference1 { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ExternalReference2 { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [Column(TypeName = "money")]
        public decimal? PhysicalTotalAmount { get; set; }
        [Column(TypeName = "money")]
        public decimal? BookTotalAmount { get; set; }
        [Column(TypeName = "money")]
        public decimal? DifferTotalAmount { get; set; }
        public long? CanceledBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CanceledDate { get; set; }
        [StringLength(2000)]
        public string CanceledReason { get; set; }
        public int? FiscalYear_ID { get; set; }
        public bool? IsPosted { get; set; }
        public int? PostedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PostedDate { get; set; }
        [StringLength(2000)]
        public string PostedComments { get; set; }
    }
}
