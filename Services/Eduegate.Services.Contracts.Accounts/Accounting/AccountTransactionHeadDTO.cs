using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Infrastructure.Enums;
using Eduegate.Services.Contracts.Accounts.Taxes;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    [DataContract]
    public class AccountTransactionHeadDTO : BaseMasterDTO
    {
        [DataMember]
        public long AccountTransactionHeadIID { get; set; }
        [DataMember]
        public Nullable<System.DateTime> TransactionDate { get; set; }
        [DataMember]
        public string TransactionNumber { get; set; }
        [DataMember]
        public Nullable<int> DocumentTypeID { get; set; }
        [DataMember]
        public DocumentReferenceTypes? DocumentReferenceTypeID { get; set; }
        [DataMember]
        public Nullable<int> PaymentModeID { get; set; }
        [DataMember]
        public Nullable<long> AccountID { get; set; }
        [DataMember]
        public string AccountCode { get; set; }
        [DataMember]
        public string AccountAlias { get; set; }
        [DataMember]
        public Nullable<long> DetailAccountID { get; set; }
        [DataMember]
        public Nullable<int> CurrencyID { get; set; }
        [DataMember]
        public decimal? ExchangeRate { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string Reference { get; set; }
        [DataMember]
        public Nullable<bool> IsPrePaid { get; set; }
        [DataMember]
        public decimal? AdvanceAmount { get; set; }
        [DataMember]
        public decimal? Amount { get; set; }
        [DataMember]
        public decimal? DiscountAmount { get; set; }
        [DataMember]
        public decimal? DiscountPercentage { get; set; }
        [DataMember]
        public decimal? TaxAmount { get; set; }
        [DataMember]
        public Nullable<int> CostCenterID { get; set; }
        [DataMember]
        public decimal? AmountPaid { get; set; }
        [DataMember]
        public Nullable<long> DocumentStatusID { get; set; }
        [DataMember]
        public Nullable<byte> TransactionStatusID { get; set; }      
        [DataMember]
        public List<AccountTransactionDetailsDTO> AccountTransactionDetails { get; set; }
        [DataMember]
        public KeyValueDTO TransactionStatus { get; set; }
        [DataMember]
        public KeyValueDTO DocumentStatus { get; set; }
        [DataMember]
        public KeyValueDTO DocumentType { get; set; }
        [DataMember]
        public KeyValueDTO PaymentModes { get; set; }
        [DataMember]
        public KeyValueDTO Currency { get; set; }
        [DataMember]
        public KeyValueDTO CostCenter { get; set; }
        [DataMember]
        public KeyValueDTO Account { get; set; }
        [DataMember]
        public KeyValueDTO DetailAccount { get; set; }

        [DataMember]
        public List<TaxDetailsDTO> TaxDetails { get; set; }

        [DataMember]
        public long? CompanyID { get; set; }

        [DataMember]
        public long? BranchID { get; set; }

        [DataMember]
        public string BranchName { get; set; }

        [DataMember]
        public bool IsTransactionCompleted { get; set; }

        [DataMember]
        public string ErrorCode { get; set; }

        [DataMember]
        public bool IsError { get; set; }

        [DataMember]
        public string ChequeNumber { get; set; }

        [DataMember]
        public DateTime? ChequeDate { get; set; }
    }
}