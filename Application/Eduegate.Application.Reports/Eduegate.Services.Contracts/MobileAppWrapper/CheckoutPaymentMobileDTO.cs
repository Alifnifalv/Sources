using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Payment;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.OrderHistory;
using Eduegate.Services.Contracts.Commons;

namespace Eduegate.Services.Contracts.MobileAppWrapper
{
    [DataContract]
    public class CheckoutPaymentMobileDTO
    {
        [DataMember]
        public OperationResultDTO operationResult { get; set; }
        [DataMember]
        public string TransactionNo { get; set; }
        [DataMember]
        public List<KeyValueDTO> RedirectValues { get; set; }

        [DataMember]
        public string RedirectUrl { get; set; }
        [DataMember]
        public string CartID { get; set; }
        [DataMember]
        public object PostObject { get; set; }

         [DataMember]
        public string DevicePlatorm { get; set; }
        [DataMember]
        public string DeviceVersion { get; set; }
        [DataMember]
        public Eduegate.Services.Contracts.Payments.PaymentDTO paymentDTO { get; set; }
        [DataMember]

        public List<OrderHistoryDTO> orderHistory { get; set; }

        [DataMember]
        public string PaymentMethodName { get; set; }

        [DataMember]
        public string DeliveryText { get; set; }

        [DataMember]
        public List<long> TransactionHeadIds { get; set; }

        [DataMember]
        public List<OrderEntitlementDTO> EntitlementMaps { get; set; }

        [DataMember]
        public long DeliveryTypeID { get; set; }

        public CheckoutPaymentMobileDTO()
        {
            TransactionNo = "";
            RedirectUrl = "";
            PostObject = new object();
            DevicePlatorm = "";
            DeviceVersion = "";
            paymentDTO = new Eduegate.Services.Contracts.Payments.PaymentDTO();
            orderHistory = new List<OrderHistoryDTO>();
        }
    }
}
