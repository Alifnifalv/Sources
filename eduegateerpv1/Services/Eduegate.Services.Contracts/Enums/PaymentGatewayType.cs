using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    public enum PaymentGatewayType
    {
        KNET = 1,
        MIGS = 2,
        MIGDEBIT = 28,
        PAYPAL = 3,
        VOUCHER = 4,
        COD = 5,
        CHEQUE = 6,
        BankTransafer = 7,
        WALLET = 8,
        CASH = 9,
        THEFORT = 10,
        CREDIT = 11,
        LOYALITYPOINTS = 14,
        MASTERCARD = 0x10,
        MASTERCARDAFTERCONFIRMORDER = 17,
        MASTERCARDDEBIT = 29,
        CCAVENUE = 18,
        OTHER = 500,
        PAYONLINEVIALINK = 20,
        WORLDLINE = 25,
        IYZICO = 26,
        CYBERSOURCE = 27,
        QPAY = 30
    }
}
