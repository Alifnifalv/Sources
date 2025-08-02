using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerGroup
    {
        public CustomerGroup()
        {
            this.ProductPriceListCustomerGroupMaps = new List<ProductPriceListCustomerGroupMap>();
            this.ProductPriceListSKUMaps = new List<ProductPriceListSKUMap>();
            this.CustomerGroupDeliveryTypeMaps = new List<CustomerGroupDeliveryTypeMap>();
        }

        public long CustomerGroupIID { get; set; }
        public string GroupName { get; set; }
        public Nullable<decimal> PointLimit { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
        public virtual ICollection<ProductPriceListSKUMap> ProductPriceListSKUMaps { get; set; }
        public virtual ICollection<CustomerGroupDeliveryTypeMap> CustomerGroupDeliveryTypeMaps { get; set; }
    }
}
