using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Asset
    {
        public Asset()
        {
            this.AssetTransactionDetails = new List<AssetTransactionDetail>();
        }

        public long AssetIID { get; set; }
        public Nullable<long> AssetCategoryID { get; set; }
        public string AssetCode { get; set; }
        public string Description { get; set; }
        public Nullable<long> AssetGlAccID { get; set; }
        public Nullable<long> AccumulatedDepGLAccID { get; set; }
        public Nullable<long> DepreciationExpGLAccId { get; set; }
        public Nullable<int> DepreciationYears { get; set; }
        public Nullable<int> Createdby { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual Account Account { get; set; }
        public virtual Account Account1 { get; set; }
        public virtual Account Account2 { get; set; }
        public virtual AssetCategory AssetCategory { get; set; }
        public virtual ICollection<AssetTransactionDetail> AssetTransactionDetails { get; set; }
    }
}
