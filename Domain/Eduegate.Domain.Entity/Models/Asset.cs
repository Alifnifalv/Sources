using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Assets", Schema = "asset")]
    public partial class Asset
    {
        public Asset()
        {
            this.AssetTransactionDetails = new List<AssetTransactionDetail>();
        }

        [Key]
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
        public virtual Account AssetGlAcc { get; set; }
        public virtual Account AccumulatedDepGLAcc { get; set; }
        public virtual Account DepreciationExpGLAcc { get; set; }
        public virtual AssetCategory AssetCategory { get; set; }
        public virtual ICollection<AssetTransactionDetail> AssetTransactionDetails { get; set; }
    }
}
