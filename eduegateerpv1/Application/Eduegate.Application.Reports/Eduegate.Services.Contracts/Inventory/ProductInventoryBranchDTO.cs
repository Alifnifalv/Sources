using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Inventory
{
    [DataContract]
    public class ProductInventoryBranchDTO
    {

        [DataMember]
        public long ProductSKUMapID { get; set; }

        [DataMember]
        public long BranchID { get; set; }

        [DataMember]
        public Decimal Quantity { get; set; }

        [DataMember]
        public bool IsMarketPlace { get; set; }

        [DataMember]
        public decimal ProductCostPrice { get; set; }

        [DataMember]
        public decimal ProductPricePrice { get; set; }

        [DataMember]
        public decimal ProductDiscountPrice { get; set; }

    }
}
