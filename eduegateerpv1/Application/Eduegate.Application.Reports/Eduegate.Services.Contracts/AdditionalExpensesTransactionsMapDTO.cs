using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public  class AdditionalExpensesTransactionsMapDTO
    {
        [DataMember]
        public long AdditionalExpensesTransactionsMapIID { get; set; }
        [DataMember]
        public int? AdditionalExpenseID { get; set; }
        [DataMember]
        public string AdditionalExpense { get; set; }
        [DataMember]
        public long? AccountID { get; set; }
        [DataMember]
        public long? ProvisionalAccountID { get; set; }
        [DataMember]
        public string ProvisionalAccount { get; set; }
        [DataMember]
        public int? ForeignCurrencyID { get; set; }
        [DataMember]
        public int? LocalCurrencyID { get; set; }
        [DataMember]
        public decimal? ExchangeRate { get; set; }
        [DataMember]
        public decimal? ForeignAmount { get; set; }
        [DataMember]
        public decimal? LocalAmount { get; set; }
        [DataMember]
        public int? CreatedBy { get; set; }
        [DataMember]
        public DateTime? CreatedDate { get; set; }
        [DataMember]
        public int? UpdatedBy { get; set; }
        [DataMember]
        public DateTime? UpdatedDate { get; set; }
        [DataMember]
        public byte? CompanyID { get; set; }
        [DataMember]
        public byte? FiscalYearID { get; set; }
        [DataMember]
        public long? AccountTransactionHeadID { get; set; }
        [DataMember]
        public long? RefInventoryTransactionHeadID { get; set; }
        [DataMember]
        public long? RefAccountTransactionHeadID { get; set; }
        [DataMember]        
        public string Remarks { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public bool? ISAffectSupplier { get; set; }
    }
}
