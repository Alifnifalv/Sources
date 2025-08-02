using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
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

        public decimal? NetAmount { get; set; }
        public long? SupplierID { get; set; }
        public decimal? TotalQuantity { get; set; }
        public long? ApproverAuthID { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? DocumentStatusID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string Remarks { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual Tender Tender { get; set; }

        public virtual ICollection<BidApprovalDetail> BidApprovalDetails { get; set; }

        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
