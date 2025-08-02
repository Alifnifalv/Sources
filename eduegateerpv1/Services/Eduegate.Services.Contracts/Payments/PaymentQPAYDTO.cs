using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Payments
{
    [DataContract]
    public class PaymentQPAYDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long PaymentQPayIID { get; set; }
        [DataMember]
        public long? LoginID { get; set; }
        [DataMember]
        public string SecureKey { get; set; }
        [DataMember]
        public string SecureHash { get; set; }
        [DataMember]
        public decimal? PaymentAmount { get; set; }
        [DataMember]
        public DateTime? TransactionRequestDate { get; set; }
        [DataMember]
        public string ActionID { get; set; }
      
        [DataMember]
        public string BankID { get; set; }
        [DataMember]
        public string NationalID { get; set; }
        [DataMember]
        public string PUN { get; set; }
        [DataMember]
        public string MerchantModuleSessionID { get; set; }
        [DataMember]
        public string MerchantID { get; set; }
        [DataMember]
        public string Lang { get; set; }
        [DataMember]
        public string CurrencyCode { get; set; }
        [DataMember]
        public string ExtraFields_f14 { get; set; }
        [DataMember]
        public int? Quantity { get; set; }
        [DataMember]
        public string PaymentDescription { get; set; }
       
        [DataMember]
        public string MerchantGatewayUrl { get; set; }
        [DataMember]
        public string ResponseAcquirerID { get; set; }
        [DataMember]
        public decimal? ResponseAmount { get; set; }
        [DataMember]
        public string ResponseBankID { get; set; }
        [DataMember]
        public string ResponseCardExpiryDate { get; set; }
        [DataMember]
        public string ResponseCardHolderName { get; set; }
        [DataMember]
        public string ResponseCardNumber { get; set; }
        [DataMember]
        public string ResponseConfirmationID { get; set; }
        [DataMember]
        public string ResponseCurrencyCode { get; set; }
        [DataMember]
        public DateTime? ResponseEZConnectResponseDate { get; set; }
        [DataMember]
        public string ResponseLang { get; set; }
        [DataMember]
        public string ResponseMerchantID { get; set; }
        [DataMember]
        public string ResponseMerchantModuleSessionID { get; set; }
        [DataMember]
        public string ResponsePUN { get; set; }
        [DataMember]
        public string ResponseStatus { get; set; }
        [DataMember]
        public string ResponseStatusMessage { get; set; }
        [DataMember]
        public string ResponseSecureHash { get; set; }
        [DataMember]
        public string LogType { get; set; }

    }
}