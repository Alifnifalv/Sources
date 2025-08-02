using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    //account.PaymentModes
    [DataContract]
    public enum PaymentModes
    {
        [EnumMember]
        Cash = 1,
        [EnumMember]
        Cheque = 2,
        [EnumMember]
        CreditCard = 3,
        [EnumMember]
        DemandDraft = 4,
        [EnumMember]
        BankTransfer_PEARL = 5,
        [EnumMember]
        OnlinePayment_CBQ = 6,
        [EnumMember]
        CreditNote = 7,
        [EnumMember]
        BankTransfer_PODAR = 8,
        [EnumMember]
        PortalOnlinePayment = 9,
        [EnumMember]
        VisaMastercard1 = 10,
        [EnumMember]
        Debit = 11,
        [EnumMember]
        VisaMasterCard = 12,
        [EnumMember]
        VisaMasterCard2 = 13,
        [EnumMember]
        QPAY = 30,
    }
}
