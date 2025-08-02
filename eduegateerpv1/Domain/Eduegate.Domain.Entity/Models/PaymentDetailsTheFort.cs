using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("PaymentDetailsTheFort", Schema = "payment")]
    public partial class PaymentDetailsTheFort
    {
        [Key]
        public long TrackID { get; set; }
        public long TrackKey { get; set; }
        public long CustomerID { get; set; }
        public long PaymentID { get; set; }
        public System.DateTime InitOn { get; set; }
        public string InitStatus { get; set; }
        public string InitIP { get; set; }
        public string InitLocation { get; set; }
        public decimal InitAmount { get; set; }
        public string PShaRequestPhrase { get; set; }
        public string PAccessCode { get; set; }
        public string PMerchantIdentifier { get; set; }
        public string PCommand { get; set; }
        public string PCurrency { get; set; }
        public string PCustomerEmail { get; set; }
        public string PLang { get; set; }
        public string PMerchantReference { get; set; }
        public string PSignatureText { get; set; }
        public string PSignature { get; set; }
        public Nullable<int> PAmount { get; set; }
        public short RefCountryID { get; set; }
        public string PTransSignature { get; set; }
        public string TransID { get; set; }
        public Nullable<System.DateTime> POn { get; set; }
        public string PTransCommand { get; set; }
        public string PTransMerchantReference { get; set; }
        public Nullable<int> PTransAmount { get; set; }
        public string PTransAccessCode { get; set; }
        public string PTransMerchantIdentifier { get; set; }
        public string PTransCurrency { get; set; }
        public string PTransPaymentOption { get; set; }
        public string PTransEci { get; set; }
        public string PTransAuthorizationCode { get; set; }
        public string PTransOrderDesc { get; set; }
        public string PTransResponseMessage { get; set; }
        public Nullable<byte> PTransStatus { get; set; }
        public Nullable<int> PTransResponseCode { get; set; }
        public string PTransCustomerIP { get; set; }
        public string PTransCustomerEmail { get; set; }
        public Nullable<short> PTransExpiryDate { get; set; }
        public string PTransCardNumber { get; set; }
        public string PTransCustomerName { get; set; }
        public Nullable<decimal> PTransActualAmount { get; set; }
        public Nullable<System.DateTime> PTransOn { get; set; }
        public Nullable<System.DateTime> TransOn { get; set; }
        public Nullable<long> OrderID { get; set; }
        public string TServiceCommand { get; set; }
        public string TAccessCode { get; set; }
        public string TSignatureText { get; set; }
        public string TSignature { get; set; }
        public string TMerchantReference { get; set; }
        public string TLanguage { get; set; }
        public string TShaRequestPhrase { get; set; }
        public Nullable<int> TAmount { get; set; }
        public string TMerchantIdentifier { get; set; }
        public string AdditionalDetails { get; set; }
        public virtual Customer Customer { get; set; }
        public Nullable<long> CartID { get; set; }
        public string CardHolderName { get; set; }
    }
}
