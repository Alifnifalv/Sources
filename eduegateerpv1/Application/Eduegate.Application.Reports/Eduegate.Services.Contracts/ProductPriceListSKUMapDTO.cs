using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class ProductPriceListSKUMapDTO
    {
        [DataMember]
        public long ProductPriceListItemMapIID { get; set; }
        [DataMember]
        public Nullable<long> ProductPriceListID { get; set; }
        [DataMember]
        public Nullable<long> ProductSKUID { get; set; }
        [DataMember]
        public Nullable<long> UnitGroundID { get; set; }
        [DataMember]
        public Nullable<long> CustomerGroupID { get; set; }
        [DataMember]
        public Nullable<decimal> SellingQuantityLimit { get; set; }
        [DataMember]
        public Nullable<decimal> Amount { get; set; }
        [DataMember]
        public Nullable<byte> SortOrder { get; set; }
        [DataMember]
        public Nullable<int> CreatedBy { get; set; }
        [DataMember]
        public Nullable<int> UpdatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [DataMember]
        public byte[] TimeStamps { get; set; }
        [DataMember]
        public Nullable<decimal> PricePercentage { get; set; }
        [DataMember]
        public Nullable<decimal> Price { get; set; }
        [DataMember]
        public Nullable<decimal> Discount { get; set; }
        [DataMember]
        public Nullable<decimal> DiscountPercentage { get; set; }
        [DataMember]
        public Nullable<decimal> Cost { get; set; }
        public Nullable<bool> IsActive { get; set; }

    }
}
