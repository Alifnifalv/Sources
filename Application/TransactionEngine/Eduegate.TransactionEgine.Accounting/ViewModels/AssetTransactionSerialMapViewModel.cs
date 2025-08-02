using System;

namespace Eduegate.TransactionEgine.Accounting.ViewModels
{
    public class AssetTransactionSerialMapViewModel
    {
        public long AssetTransactionSerialMapIID { get; set; }

        public long AssetSerialMapID { get; set; }

        public string AssetSequenceCode { get; set; }

        public string SerialNumber { get; set; }

        public string AssetTag { get; set; }

        public long? TransactionDetailID { get; set; }

        public long? AssetID { get; set; }

        public bool IsError { get; set; }

        public string ErrorMessage { get; set; }

        public decimal? CostPrice { get; set; }

        public int? ExpectedLife { get; set; }

        public decimal? DepreciationRate { get; set; }

        public decimal? ExpectedScrapValue { get; set; }

        public decimal? AccumulatedDepreciationAmount { get; set; }

        public DateTime? DateOfFirstUse { get; set; }

        public long? SupplierID { get; set; }

        public string BillNumber { get; set; }

        public DateTime? BillDate { get; set; }
    }
}