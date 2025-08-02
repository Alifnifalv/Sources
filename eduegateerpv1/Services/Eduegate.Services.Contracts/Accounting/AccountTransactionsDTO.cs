using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounting
{
    [DataContract]
    public class AccountTransactionsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long TransactionIID { get; set; }
        [DataMember]
        public int? DocumentTypeID { get; set; }
        [DataMember]
        public string TransactionNumber { get; set; }
        [DataMember]
        public int? CostCenterID { get; set; }
        [DataMember]
        public Nullable<long> AccountID { get; set; }
        [DataMember]
        public Nullable<decimal> Amount { get; set; }
        [DataMember]
        public Nullable<decimal> InclusiveTaxAmount { get; set; }
        [DataMember]
        public Nullable<decimal> ExclusiveTaxAmount { get; set; }
        [DataMember]
        public Nullable<decimal> DiscountAmount { get; set; }
        [DataMember]
        public Nullable<decimal> DiscountPercentage { get; set; }
        [DataMember]
        public Nullable<bool> DebitOrCredit { get; set; }
        [DataMember]
        public TransactionHeadAccountMapDTO TransactionHeadAccountMap { get; set; }
        [DataMember]
        public Nullable<long> TransactionHeadID { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int TransactionType { get; set; }
        [DataMember]
        public DateTime? TransactionDate { get; set; }
        [DataMember]
        public DateTime? DueDate { get; set; }
        [DataMember]
        public int? BudgetID { get; set; }
        
    }
}
