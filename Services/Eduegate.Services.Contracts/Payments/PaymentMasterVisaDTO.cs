using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Payments
{
    public class PaymentMasterVisaDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long TrackIID { get; set; }

        [DataMember]
        public long TrackKey { get; set; }

        [DataMember]
        public long? CustomerID { get; set; }

        [DataMember]
        public long PaymentID { get; set; }

        [DataMember]
        public string SecureHash { get; set; }

        [DataMember]
        public DateTime? InitOn { get; set; }

        [DataMember]
        public string InitStatus { get; set; }

        [DataMember]
        public string InitIP { get; set; }

        [DataMember]
        public string InitLocation { get; set; }

        [DataMember]
        public string VpcURL { get; set; }

        [DataMember]
        public string VpcVersion { get; set; }

        [DataMember]
        public string VpcCommand { get; set; }

        [DataMember]
        public string AccessCode { get; set; }

        [DataMember]
        public string MerchantID { get; set; }

        [DataMember]
        public string VpcLocale { get; set; }

        [DataMember]
        public decimal? PaymentAmount { get; set; }

        [DataMember]
        public int? VirtualAmount { get; set; }

        [DataMember]
        public string PaymentCurrency { get; set; }

        [DataMember]
        public DateTime? ResponseOn { get; set; }

        [DataMember]
        public string ResponseCode { get; set; }

        [DataMember]
        public string CodeDescription { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string ReceiptNumber { get; set; }

        [DataMember]
        public string TransID { get; set; }

        [DataMember]
        public string AcquireResponseCode { get; set; }

        [DataMember]
        public string BankAuthorizationID { get; set; }

        [DataMember]
        public string BatchNo { get; set; }

        [DataMember]
        public string CardType { get; set; }

        [DataMember]
        public int? ResponseAmount { get; set; }

        [DataMember]
        public string ResponseIP { get; set; }

        [DataMember]
        public string ResponseLocation { get; set; }

        [DataMember]
        public long? OrderID { get; set; }

        [DataMember]
        public long? CartID { get; set; }

        [DataMember]
        public int? CardTypeID { get; set; }

        [DataMember]
        public string Response { get; set; }

        [DataMember]
        public string LogType { get; set; }

        [DataMember]
        public bool? IsMasterVisaSaved { get; set; }

        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        public bool? SuccessStatus { get; set; }

        #region Mobile app use
        [DataMember]
        public string BankSession { get; set; }

        [DataMember]
        public string MerchantName { get; set; }

        [DataMember]
        public string OrderDescription { get; set; }

        [DataMember]
        public string MerchantCheckoutJS { get; set; }

        [DataMember]
        public string MerchantLogoURL { get; set; }

        [DataMember]
        public string MerchantCardURL { get; set; }
        [DataMember]
        public string ResponseAcquirerID { get; set; }
        [DataMember]
        public string ResponseBankID { get; set; }
        [DataMember]
        public string ResponseCardExpiryDate { get; set; }
        [DataMember]
        public string ResponseCardHolderName { get; set; }
        [DataMember]
        public string ResponseConfirmationID { get; set; }
        [DataMember]
        public string ResponseStatus { get; set; }
        [DataMember]
        public string ResponseStatusMessage { get; set; }
        [DataMember]
        public string ResponseSecureHash { get; set; }

        #endregion
    }
}