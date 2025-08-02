using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Eduegate.Domain.Entity.Models
{
    [Table("PaymentMasterVisa", Schema = "payment")]
    public partial class PaymentMasterVisa
    {
        [Key]
        public long TrackIID { get; set; }

        public long TrackKey { get; set; }

        public long? CustomerID { get; set; }

        public string SecureHash { get; set; }

        public long PaymentID { get; set; }

        public DateTime? InitOn { get; set; }

        public string InitStatus { get; set; }

        public string InitIP { get; set; }

        public string InitLocation { get; set; }

        public string VpcURL { get; set; }

        public string VpcVersion { get; set; }

        public string VpcCommand { get; set; }

        public string AccessCode { get; set; }

        public string MerchantID { get; set; }

        public string VpcLocale { get; set; }

        public decimal? PaymentAmount { get; set; }

        public int? VirtualAmount { get; set; }
       
        public string PaymentCurrency { get; set; }

        public DateTime? ResponseOn { get; set; }

        public string ResponseCode { get; set; }

        public string CodeDescription { get; set; }

        public string Message { get; set; }

        public string ReceiptNumber { get; set; }

        public string TransID { get; set; }

        public string AcquireResponseCode { get; set; }

        public string BankAuthorizationID { get; set; }

        public string BatchNo { get; set; }

        public string CardType { get; set; }

        public int? ResponseAmount { get; set; }

        public string ResponseIP { get; set; }

        public string ResponseLocation { get; set; }

        public long? OrderID { get; set; }

        public long? CartID { get; set; }

        public string Response { get; set; }

        public string LogType { get; set; }

        public int? CardTypeID { get; set; }

        public string ResponseAcquirerID { get; set; }
        public string ResponseBankID { get; set; }

        public string ResponseCardExpiryDate { get; set; }
        public string ResponseCardHolderName { get; set; }

        public string ResponseConfirmationID { get; set; }
        public string ResponseStatus { get; set; }

        public string ResponseStatusMessage { get; set; }
        public string ResponseSecureHash { get; set; }

        public long? LoginID { get; set; }

        public bool? SuccessStatus { get; set; }

        public virtual Login Login { get; set; }

    }
}