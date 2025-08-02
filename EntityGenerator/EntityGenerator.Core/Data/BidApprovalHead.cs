using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BidApprovalHead", Schema = "inventory")]
    public partial class BidApprovalHead
    {
        public BidApprovalHead()
        {
            BidApprovalDetails = new HashSet<BidApprovalDetail>();
            TransactionHeads = new HashSet<TransactionHead>();
        }

        [Key]
        public long BidApprovalIID { get; set; }
        public int? DocumentTypeID { get; set; }
        public string Description { get; set; }
        [StringLength(250)]
        public string TransactionNo { get; set; }
        public long? TenderID { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? NetAmount { get; set; }
        public long? SupplierID { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? TotalQuantity { get; set; }
        public long? ApproverAuthID { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public long? DocumentStatusID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? DiscountAmount { get; set; }
        public string Remarks { get; set; }

        [ForeignKey("DocumentStatusID")]
        [InverseProperty("BidApprovalHeads")]
        public virtual DocumentStatus DocumentStatus { get; set; }
        [ForeignKey("DocumentTypeID")]
        [InverseProperty("BidApprovalHeads")]
        public virtual DocumentType DocumentType { get; set; }
        [ForeignKey("TenderID")]
        [InverseProperty("BidApprovalHeads")]
        public virtual Tender Tender { get; set; }
        [InverseProperty("BidApproval")]
        public virtual ICollection<BidApprovalDetail> BidApprovalDetails { get; set; }
        [InverseProperty("Bid")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
