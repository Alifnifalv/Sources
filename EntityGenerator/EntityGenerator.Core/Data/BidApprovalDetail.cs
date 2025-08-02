using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BidApprovalDetail", Schema = "inventory")]
    public partial class BidApprovalDetail
    {
        [Key]
        public long DetailIID { get; set; }
        public long? BidApprovalID { get; set; }
        public long? ReferenceHeadID { get; set; }
        public long? ProductID { get; set; }
        public long? ProductSKUMapID { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Quantity { get; set; }
        public long? UnitID { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? UnitPrice { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Fraction { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? ForiegnRate { get; set; }
        public long? UnitGroupID { get; set; }
        public string Remark { get; set; }

        [ForeignKey("BidApprovalID")]
        [InverseProperty("BidApprovalDetails")]
        public virtual BidApprovalHead BidApproval { get; set; }
    }
}
