using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("mutual.CustomerGroups")]
    public partial class CustomerGroup
    {
        public CustomerGroup()
        {
            this.Customers = new List<Customer>();
            this.BrandPriceListMaps = new List<BrandPriceListMap>();
            this.CategoryPriceListMaps = new List<CategoryPriceListMap>();
            this.ProductPriceListSKUMaps = new List<ProductPriceListSKUMap>();
            this.CustomerGroupDeliveryTypeMaps = new List<CustomerGroupDeliveryTypeMap>();
            this.ProductPriceListCustomerGroupMaps = new List<ProductPriceListCustomerGroupMap>();
        }

        public long CustomerGroupIID { get; set; }
        public string GroupName { get; set; }
        public Nullable<decimal> PointLimit { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual ICollection<BrandPriceListMap> BrandPriceListMaps { get; set; }
        public virtual ICollection<CategoryPriceListMap> CategoryPriceListMaps { get; set; }
        public virtual ICollection<ProductPriceListSKUMap> ProductPriceListSKUMaps { get; set; }
        public virtual ICollection<CustomerGroupDeliveryTypeMap> CustomerGroupDeliveryTypeMaps { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
    }
}