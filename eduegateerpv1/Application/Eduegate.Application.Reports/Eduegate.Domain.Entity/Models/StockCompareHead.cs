using Eduegate.Domain.Entity.Models.Inventory;
using Eduegate.Domain.Entity.Models.Workflows;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("inventory.StockCompareHead")]
    public partial class StockCompareHead
    {
        [Key]
        public long HeadIID { get; set; }

        public int? CompanyID { get; set; }

        public long? BranchID { get; set; }

        public int? DocumentTypeID { get; set; }

        [StringLength(50)]
        public string TransactionNo { get; set; }

        public DateTime? TransactionDate { get; set; }

        public byte? TransactionStatusID { get; set; }

        public long? ReferenceHeadID { get; set; }

        public int? CurrencyID { get; set; }

        public decimal? ExchangeRate { get; set; }

        public long? EmployeeID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] DocumentStatusID { get; set; }

        [StringLength(50)]
        public string Reference { get; set; }

        public DateTime? DocumentCancelledDate { get; set; }

        [StringLength(50)]
        public string ExternalReference1 { get; set; }

        [StringLength(50)]
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

        public DateTime? CanceledDate { get; set; }

        [StringLength(2000)]
        public string CanceledReason { get; set; }

        public int? FiscalYear_ID { get; set; }

        public bool? IsPosted { get; set; }

        public int? PostedBy { get; set; }

        public DateTime? PostedDate { get; set; }

        [StringLength(2000)]
        public string PostedComments { get; set; }
    }
}