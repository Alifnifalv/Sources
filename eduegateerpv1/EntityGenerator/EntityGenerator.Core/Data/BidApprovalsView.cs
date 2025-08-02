using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class BidApprovalsView
    {
        public long BidApprovalIID { get; set; }
        public int? DocumentTypeID { get; set; }
        [StringLength(250)]
        public string TransactionNo { get; set; }
        public string TenderName { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? NetAmount { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? TotalQuantity { get; set; }
        public string ApprovedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public long? LoginID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
    }
}
