using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{

    [DataContract]
    public class WalletTransactionDTO
    {

        
        [DataMember]
        public string TransactionDescription { get; set; }
        [DataMember]
        public string TransactionDateAndTime { get; set; }
        [DataMember]
        public string Amount { get; set; }
        [DataMember]
        public string TransactionReference { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public Int64? PaymentId { get; set; }
        [DataMember]
        public Int64? TransId { get; set; }
        [DataMember]
        public string PaymentMethod { get; set; }
        [DataMember]
        public string ReferenceID { get; set; }
        [DataMember]
        public string VoucherNumber { get; set; }
        [DataMember]
        public short StatusId { get; set; }
    }

    [DataContract]
    public class WalletCustomerDTO
    {
        [DataMember]
        public string WalletGuid { get; set; }
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string CustomerFirstName { get; set; }
        [DataMember]
        public string CustomerLastName { get; set; }
        [DataMember]
        public string CustomerEmailId { get; set; }
        [DataMember]
        public string CustomerSessionId { get; set; }

    }

    [DataContract(Name = "TransactionRelation")]
    public enum TransactionRelationEnum
    {
        [EnumMember]
        UsedWalletForOrder = 1,
        [EnumMember]
        WalletCredit = 2,

    }

    [DataContract]
    public partial class VoucherTransactionDTO
    {
        [DataMember]
        public int TransID { get; set; }
        [DataMember]
        public string VoucherNo { get; set; }
        [DataMember]
        public long RefOrderID { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
    }


    [DataContract]
    public partial class VoucherWalletTransactionDTO
    {
        [DataMember]
        public long TransID { get; set; }
        [DataMember]
        public string VoucherNo { get; set; }
        [DataMember]
        public long WalletTransactionID { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
    }
}
