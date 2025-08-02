using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.OrderHistory
{
    public class OrderDetailDTO
    {
        [DataMember]
        public long TransactionIID { get; set; }

        [DataMember]
        public Nullable<long> ProductID { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string SerialNumber { get; set; }

        [DataMember]
        public string PartNumber { get; set; }

        [DataMember]
        public string ProductImageUrl { get; set; }

        [DataMember]
        public string ProductUrl { get; set; }

        [DataMember]
        public Nullable<long> ProductSKUMapID { get; set; }

        [DataMember]
        public Nullable<decimal> Quantity { get; set; }

        [DataMember]
        public Nullable<long> UnitID { get; set; }

        [DataMember]
        public Nullable<decimal> DiscountPercentage { get; set; }

        [DataMember]
        public Nullable<decimal> UnitPrice { get; set; }

        [DataMember]
        public Nullable<decimal> Amount { get; set; }

        [DataMember]
        public Nullable<decimal> ExchangeRate { get; set; }

        [DataMember]
        public List<KeyValuePair<byte, string>> Properties { get; set; }

        [DataMember]
        public List<KeyValuePair<long, string>> Categories { get; set; }
        [DataMember]
        public List<EditOrderDetailDTO> EditOrderDetails { get; set; }

        [DataMember]
        public int? Action { get; set; }

        [DataMember]
        public long DetailIID { get; set; }
        [DataMember]
        public Nullable<decimal> CancelQuantity { get; set; }
        [DataMember]
        public Nullable<decimal> ActualQuantity { get; set; }

        public OrderDetailDTO()
        {
            EditOrderDetails = new List<EditOrderDetailDTO>();
        }
    }
}
