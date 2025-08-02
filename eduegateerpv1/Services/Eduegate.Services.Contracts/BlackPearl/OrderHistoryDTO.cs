using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class OrderHistoryDTO
    {
        [DataMember]
        public long TransactionOrderIID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string TransactionNo { get; set; }

        [DataMember]
        public Nullable<System.DateTime> TransactionDate { get; set; }

        [DataMember]
        public Nullable<int> DocumentTypeID { get; set; }

        [DataMember]
        public Nullable<long> CustomerID { get; set; }

        [DataMember]
        public Nullable<long> SupplierID { get; set; }

        [DataMember]
        public List<OrderDetailDTO> OrderDetails { get; set; }

        [DataMember]
        public UserDTO UserDetail { get; set; }

        [DataMember]
        public string PaymentMethod { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public decimal SubTotal { get; set; }

        [DataMember]
        public decimal VoucherAmount { get; set; }
        [DataMember]
        public Nullable<decimal> DeliveryCharge { get; set; }
        [DataMember]
        public Nullable<decimal> DiscountAmount { get; set; }
        [DataMember]
        public Nullable<decimal> DiscountPercentage { get; set; }
        [DataMember]
        public Nullable<decimal> Discount { get; set; }
        [DataMember]
        public TransactionStatus TransactionStatus { get; set; }
    }
}
