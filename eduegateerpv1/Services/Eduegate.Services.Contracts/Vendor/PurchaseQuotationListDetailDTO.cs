using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Vendor
{
    public class PurchaseQuotationListDetailDTO
    {
        [DataMember]
        public long? DetailIID { get; set; }
        [DataMember]
        public long? ProductID { get; set; }
        [DataMember]
        public long? ProductSKUMapID { get; set; }
        [DataMember]
        public int? SerialNo { get; set; } 
        [DataMember]
        public string Product { get; set; }
        [DataMember]
        public string ProductCode { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public decimal? Quantity { get; set; }
        [DataMember]
        public string Unit { get; set; }
        [DataMember]
        public decimal? Price { get; set; }
        [DataMember]
        public decimal? Amount { get; set; }
    }
}
