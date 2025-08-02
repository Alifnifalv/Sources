using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
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
        public decimal? Quantity { get; set; }
        public long? UnitID { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Fraction { get; set; }
        public decimal? ForiegnRate { get; set; }
        public long? UnitGroupID { get; set; }
        public string Remark { get; set; }

        public virtual BidApprovalHead BidApproval { get; set; }
    }
}
