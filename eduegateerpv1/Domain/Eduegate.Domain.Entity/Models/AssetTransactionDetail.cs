using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("AssetTransactionDetails", Schema = "asset")]
    public partial class AssetTransactionDetail
    {
        [Key]
        public long DetailIID { get; set; }
        public Nullable<long> HeadID { get; set; }
        public Nullable<long> AssetID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> AccountID { get; set; }
        public virtual Account Account { get; set; }
        public virtual Asset Asset { get; set; }
        public virtual AssetTransactionHead AssetTransactionHead { get; set; }
    }
}
