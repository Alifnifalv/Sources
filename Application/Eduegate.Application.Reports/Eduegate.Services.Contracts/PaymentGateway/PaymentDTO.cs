using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Eduegate.Framework.Extensions;
using System.Runtime.Serialization;
using Eduegate.Framework.Payment;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class PaymentDTO
    {
        [DataMember]
        public string Guid { get; set; }
        [DataMember]
        public string Amount { get; set; }
        [DataMember]
        public string CustomerID { get; set; }
        [DataMember]
        public string EmailId { get; set; }
        [DataMember]
        public string PaymentID { get; set; }
        [DataMember]
        public string TrackKey { get; set; }
        [DataMember]
        public string TrackID { get; set; }
        [DataMember]
        public string SessionID { get; set; }
        [DataMember]
        public string TransactionStatus { get; set; }
        [DataMember]
        public string InitiatedOn { get; set; }
        [DataMember]
        public string InitiatedFromIP { get; set; }
        [DataMember]
        public string InitiatedLocation { get; set; }
        [DataMember]
        public string PaymentGatewayUrl { get; set; }
        [DataMember]
        public string SuccessReturnUrl { get; set; }
        [DataMember]
        public string CancelReturnUrl { get; set; }
        [DataMember]
        public string FailureReturnUrl { get; set; }
        [DataMember]
        public string OrderID { get; set; }
        [DataMember]
        public string AdditionalDetails { get; set; }
        [DataMember]
        public PaymentGatewayType PaymentGateway { get; set; }
        [DataMember]
        public string CustomAttributes { get; set; }
        [DataMember]
        public VoucherWalletTransactionDTO VoucherTransactionDetail { get; set; }

    }



}
