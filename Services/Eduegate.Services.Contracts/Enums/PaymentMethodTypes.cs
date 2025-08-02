using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "PaymentMethodTypes")]
    public enum PaymentMethodTypes
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
        VisaMastercard = 6,
        [EnumMember]
        Voucher = 7,
        [EnumMember]
        Wallet = 8,
        [EnumMember]
        VISA = 9,
        [EnumMember]
        MasterCard = 10,
        [EnumMember]
        OnAccount = 11
    }
}
