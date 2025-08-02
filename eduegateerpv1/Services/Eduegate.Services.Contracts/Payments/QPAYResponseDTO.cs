using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Eduegate.Services.Contracts.Payments
{
    [DataContract]
    internal class QPAYResponseDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public string CardExpiryDate { get; set; }
        [DataMember]
        public string CardHolderName { get; set; }
        [DataMember]
        public string CardNumber { get; set; }
        [DataMember]
        public string secretKey { get; set; }
        [DataMember]
        public string AcquirerID { get; set; }
        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string StatusMessage { get; set; }
        [DataMember]
        public string StackTrace { get; set; }
        [DataMember]
        public string InnerErrorMessage { get; set; }
        [DataMember]
        public string ConfirmationID { get; set; }

        [DataMember]
        public string transactionRequestDate { get; set; }
        [DataMember]
        public int ActionID { get; set; }
        [DataMember]
        public decimal? Amount { get; set; }
        [DataMember]
        public string BankID { get; set; }
        [DataMember]
        public string NationalID { get; set; }
        [DataMember]
        public string PUN { get; set; }
        [DataMember]
        public string MerchantModuleSessionID { get; set; }
        [DataMember]
        public long MerchantID { get; set; }
        [DataMember]
        public string Lang { get; set; }
        [DataMember]
        public string CurrencyCode { get; set; }
        [DataMember]
        public string ExtraFields_f14 { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public string PaymentDescription { get; set; }

    }
}