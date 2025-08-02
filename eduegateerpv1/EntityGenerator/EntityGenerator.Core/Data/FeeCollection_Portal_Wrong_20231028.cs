using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeCollection_Portal_Wrong_20231028
    {
        public long LoginIID { get; set; }
        [StringLength(100)]
        public string LoginUserID { get; set; }
        [StringLength(100)]
        public string LoginEmailID { get; set; }
        public long FeeCollectionIID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public long? StudentID { get; set; }
        public int? FeeMasterID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CollectionDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? FineAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PaidAmount { get; set; }
        public bool? IsPaid { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(50)]
        public string FeeReceiptNo { get; set; }
        public long? ClassFeeMasterId { get; set; }
        public bool IsAccountPosted { get; set; }
        public int? AcadamicYearID { get; set; }
        public long? AccountTransactionHeadID { get; set; }
        public byte? SchoolID { get; set; }
        public long? CashierID { get; set; }
        public string Remarks { get; set; }
        public bool? IsCancelled { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CancelledDate { get; set; }
        [StringLength(250)]
        public string CancelReason { get; set; }
        public int? FeeCollectionStatusID { get; set; }
        [StringLength(50)]
        public string GroupTransactionNumber { get; set; }
    }
}
