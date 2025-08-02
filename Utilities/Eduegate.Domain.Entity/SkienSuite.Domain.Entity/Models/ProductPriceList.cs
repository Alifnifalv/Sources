using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductPriceList
    {
        public ProductPriceList()
        {
            this.ProductPriceListBranchMaps = new List<ProductPriceListBranchMap>();
            this.ProductPriceListBrandMaps = new List<ProductPriceListBrandMap>();
            this.ProductPriceListCategoryMaps = new List<ProductPriceListCategoryMap>();
            this.ProductPriceListCustomerGroupMaps = new List<ProductPriceListCustomerGroupMap>();
            this.ProductPriceListCustomerMaps = new List<ProductPriceListCustomerMap>();
            this.ProductPriceListProductMaps = new List<ProductPriceListProductMap>();
            this.ProductPriceListSKUMaps = new List<ProductPriceListSKUMap>();
        }

        public long ProductPriceListIID { get; set; }
        public string PriceDescription { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> PricePercentage { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<short> ProductPriceListTypeID { get; set; }
        public Nullable<short> ProductPriceListLevelID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual ICollection<ProductPriceListBranchMap> ProductPriceListBranchMaps { get; set; }
        public virtual ICollection<ProductPriceListBrandMap> ProductPriceListBrandMaps { get; set; }
        public virtual ICollection<ProductPriceListCategoryMap> ProductPriceListCategoryMaps { get; set; }
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
        public virtual ICollection<ProductPriceListCustomerMap> ProductPriceListCustomerMaps { get; set; }
        public virtual ProductPriceListLevel ProductPriceListLevel { get; set; }
        public virtual ICollection<ProductPriceListProductMap> ProductPriceListProductMaps { get; set; }
        public virtual ICollection<ProductPriceListSKUMap> ProductPriceListSKUMaps { get; set; }
        public virtual ProductPriceListType ProductPriceListType { get; set; }
    }
}
