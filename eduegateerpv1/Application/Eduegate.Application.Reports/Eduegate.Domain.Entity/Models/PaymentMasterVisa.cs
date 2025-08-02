using System;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PaymentMasterVisa
    {
        public long TrackIID { get; set; }

        public long TrackKey { get; set; }

        public long CustomerID { get; set; }

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
    }
}