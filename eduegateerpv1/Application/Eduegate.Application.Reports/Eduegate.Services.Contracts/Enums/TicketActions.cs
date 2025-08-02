using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract]
    public enum TicketActions
    {
        [EnumMember]
        Refund = 1,
        [EnumMember]
        CollectItem = 2,
        [EnumMember]
        DirectReplacement = 3,
        [EnumMember]
        Arrangement = 4,
        [EnumMember]
        DigitalCard = 5,
        [EnumMember]
        AmountCapture = 6,
        [EnumMember]
        Voucher = 7,
        [EnumMember]
        RefundByDriver = 8,
        [EnumMember]
        CashRefundShowroom = 9,
        [EnumMember]
        BankDeposit = 10,
        [EnumMember]
        CollectItemServiceCenter = 11,
        [EnumMember]
        CollectItemWarehouse = 12,
        [EnumMember]
        DirectReplacementServiceCenter = 13,
        [EnumMember]
        DirectReplacementWarehouse = 14,
        [EnumMember]
        InvalidCode = 15,
        [EnumMember]
        Redeemed = 16,
        [EnumMember]
        Activated = 17,
        [EnumMember]
        ArrangmentPM = 18,
        [EnumMember]
        ArrangementPurchase = 19,
        [EnumMember]
        ArrangementWarehouse = 20,
        [EnumMember]
        ArrangementDistribution = 21,
    }
}
