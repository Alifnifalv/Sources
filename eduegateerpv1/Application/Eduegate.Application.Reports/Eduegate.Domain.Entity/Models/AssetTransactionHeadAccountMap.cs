using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class AssetTransactionHeadAccountMap
    {
        public long AssetTransactionHeadAccountMapIID { get; set; }
        public long AccountTransactionID { get; set; }
        public long AssetTransactionHeadID { get; set; }
        public virtual AccountTransaction AccountTransaction { get; set; }
        public virtual AssetTransactionHead AssetTransactionHead { get; set; }
    }
}
