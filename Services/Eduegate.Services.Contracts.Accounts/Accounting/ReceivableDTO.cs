using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    [DataContract]
   public  class ReceivableDTO : BaseMasterDTO
    {
        public ReceivableDTO()
        {
            ReferenceReceivables = new List<ReceivableDTO>();
        }

        [DataMember]
        public long ReceivableIID { get; set; }
        [DataMember]
        public Nullable<int> DocumentReferenceTypeID { get; set; }
        [DataMember]
        public Nullable<int> DocumentTypeID { get; set; }
        [DataMember]
        public string TransactionNumber { get; set; }
        [DataMember]
        public string DocumentTypeName { get; set; }
        [DataMember]
        public Nullable<System.DateTime> TransactionDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> DueDate { get; set; }
        [DataMember]
        public Nullable<long> SerialNumber { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public Nullable<long> ReferenceReceivablesID { get; set; }
        [DataMember]
        public Nullable<long> DocumentStatusID { get; set; }
        [DataMember]
        public Nullable<long> AccountID { get; set; }
        [DataMember]
        public Nullable<decimal> Amount { get; set; }
        [DataMember]
        public Nullable<decimal> PaidAmount { get; set; }
        [DataMember]
        public Nullable<decimal> ReturnAmount { get; set; }
        [DataMember]
        public Nullable<System.DateTime> AccountPostingDate { get; set; }
        [DataMember]
        public Nullable<decimal> ExchangeRate { get; set; }
        [DataMember]
        public Nullable<int> CurrencyID { get; set; }
        [DataMember]
        public Nullable<byte> TransactionStatusID { get; set; }
        [DataMember]
        public KeyValueDTO Account { get; set; }
        [DataMember]
        public KeyValueDTO Currency { get; set; }
        [DataMember]
        public KeyValueDTO DocumentStatus { get; set; }
        [DataMember]
        public KeyValueDTO Receivable1 { get; set; }
        [DataMember]
        public KeyValueDTO TransactionStatus { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public Nullable<decimal> InvoiceAmount { get; set; }
        [DataMember]
        public string CurrencyName { get; set; }
        [DataMember]
        public bool? DebitOrCredit { get; set; }
        [DataMember]
        public List<TransactionHeadReceivablesMapDTO> TransactionHeadReceivablesMaps { get; set; }
        [DataMember]
        public List<AccountTransactionReceivablesMapDTO> AccountTransactionReceivablesMaps { get; set; }

        [DataMember]
        public List<ReceivableDTO> ReferenceReceivables { get; set; }
        //[DataMember]
        //public decimal ExchangeRate { get; set; }

        [DataMember]
        public Nullable<decimal> DiscountAmount { get; set; }
    }
}
