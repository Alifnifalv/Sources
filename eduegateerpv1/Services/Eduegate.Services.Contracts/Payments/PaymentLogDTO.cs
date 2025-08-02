using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Payments
{
    [DataContract]
    public class PaymentLogDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long PaymentLogIID { get; set; }

        [DataMember]
        public long? TrackID { get; set; }

        [DataMember]
        public long? TrackKey { get; set; }

        [DataMember]
        public string RequestLog { get; set; }

        [DataMember]
        public string ResponseLog { get; set; }

        [DataMember]
        public int? CompanyID { get; set; }

        [DataMember]
        public int? SiteID { get; set; }

        [DataMember]
        public string RequestUrl { get; set; }

        [DataMember]
        [StringLength(50)]
        public string RequestType { get; set; }

        [DataMember]
        public long? CartID { get; set; }

        [DataMember]
        public long? CustomerID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public string TransNo { get; set; }

        [DataMember]
        public string ValidationResult { get; set; }
        [DataMember]
        public string CardType { get; set; }
        [DataMember]
        public int   CardTypeID { get; set; }
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

        [DataMember]
        public long? LoginID { get; set; }

    }
}