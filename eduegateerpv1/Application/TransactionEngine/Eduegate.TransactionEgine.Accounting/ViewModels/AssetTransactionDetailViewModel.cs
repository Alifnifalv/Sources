using System;
using System.Collections.Generic;

namespace Eduegate.TransactionEgine.Accounting.ViewModels
{
    public partial class AssetTransactionDetailViewModel
    {
        public long DetailIID { get; set; }
        public Nullable<long> HeadID { get; set; }
        public Nullable<long> AssetID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> Amount { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> AccountID { get; set; }
        public AssetTransactionHeadViewModel AssetTransactionHead { get; set; }

    }
}
