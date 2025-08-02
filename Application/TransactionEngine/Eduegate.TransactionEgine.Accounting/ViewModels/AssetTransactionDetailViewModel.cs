using System;
using System.Collections.Generic;

namespace Eduegate.TransactionEgine.Accounting.ViewModels
{
    public partial class AssetTransactionDetailViewModel
    {
        public long DetailIID { get; set; }

        public long? HeadID { get; set; }

        public long? AssetID { get; set; }

        public decimal? Quantity { get; set; }

        public int? Amount { get; set; }

        public long? AccountID { get; set; }

        public AssetTransactionHeadViewModel AssetTransactionHead { get; set; }

        public long? AssetSerialMapID { get; set; }

        public long? ProductSKUMapID { get; set; }

        public long? BatchID { get; set; }

        public decimal? CostAmount { get; set; }

        public DateTime? CutOffDate { get; set; }

        public List<AssetTransactionSerialMapViewModel> AssetTransactionSerialMaps { get; set; }

    }
}