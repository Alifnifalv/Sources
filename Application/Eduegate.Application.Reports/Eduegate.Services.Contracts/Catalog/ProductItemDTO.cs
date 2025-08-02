using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class ProductItemDTO
    {
        [DataMember]
        public decimal IID { get; set; }

        [DataMember]
        public string CreatedOn { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public long Quantity { get; set; }

        [DataMember]
        public long PartNo { get; set; }

        [DataMember]
        public long DeliveryDays { get; set; }

        [DataMember]
        public string BarCode { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string Brand { get; set; }

        [DataMember]
        public long Price { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }

        [DataMember]
        public decimal DiscountPercentage { get; set; }
    }
}
