using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    // mutual.PaymentMethods 
    [DataContract]
    public enum PaymentMethods
    {
        [EnumMember]
        BankTransfer = 1,
        [EnumMember]
        Cheque = 2,
        [EnumMember]
        Cash = 3,
        [EnumMember]
        KNET = 4,
        [EnumMember]
        Paypal = 5,
        [EnumMember]
        VisaMasterCard = 6,
        [EnumMember]
        Voucher = 7,
        [EnumMember]
        Wallet = 8,
    }
}
