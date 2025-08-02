using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public partial class InvetoryTransactionDTO
    {
        [DataMember]
        public long InventoryTransactionIID { get; set; }
        [DataMember]
        public Nullable<int> SerialNo { get; set; }
        [DataMember]
        public Nullable<int> DocumentTypeID { get; set; }
        [DataMember]
        public string TransactionNo { get; set; }
        [DataMember]
        public string TransactionDate { get; set; }
        [DataMember]
        public Nullable<long> ProductSKUMapID { get; set; }
        [DataMember]
        public Nullable<long> AccountID { get; set; }
        [DataMember]
        public Nullable<long> UnitID { get; set; }
        [DataMember]
        public Nullable<decimal> Cost { get; set; }
        [DataMember]
        public Nullable<decimal> Quantity { get; set; }
        [DataMember]
        public Nullable<decimal> Amount { get; set; }
        [DataMember]
        public Nullable<decimal> Rate { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public Nullable<decimal> Discount { get; set; }
        [DataMember]
        public Nullable<System.DateTime> DueDate { get; set; }
        [DataMember]
        public Nullable<long> CurrencyID { get; set; }
        [DataMember]
        public Nullable<decimal> ExchangeRate { get; set; }
        [DataMember]
        public Nullable<long> LinkDocumentID { get; set; }
        [DataMember]
        public Nullable<long> BranchID { get; set; }
        [DataMember]
        public Nullable<long> BatchID { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }
        [DataMember]
        public Nullable<long> HeadID { get; set; }
        [DataMember]
        public Nullable<decimal> LandingCost { get; set; }
        [DataMember]
        public Nullable<decimal> LastCostPrice { get; set; }
        [DataMember]
        public Nullable<decimal> Fraction { get; set; }
        [DataMember]
        public Nullable<decimal> OriginalQty { get; set; }

    }

}
