using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Security.Secured;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductPriceListSKUMaps", Schema = "catalog")]
    public partial class ProductPriceListSKUMap : ISecuredEntity
    {
        public ProductPriceListSKUMap()
        {
            this.ProductPriceListSKUQuantityMaps = new List<ProductPriceListSKUQuantityMap>();
        }

        [Key]
        public long ProductPriceListItemMapIID { get; set; }
        public Nullable<long> ProductPriceListID { get; set; }
        public Nullable<long> ProductSKUID { get; set; }
        public Nullable<long> UnitGroundID { get; set; }
        public Nullable<long> CustomerGroupID { get; set; }
        public Nullable<decimal> SellingQuantityLimit { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<byte> SortOrder { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        ////public byte[] TimeStamps { get; set; }
        public Nullable<decimal> PricePercentage { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public virtual ProductPriceList ProductPriceList { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        public virtual UnitGroup UnitGroup { get; set; }
        public virtual CustomerGroup CustomerGroup { get; set; }
        public virtual ICollection<ProductPriceListSKUQuantityMap> ProductPriceListSKUQuantityMaps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public long GetIID()
        {
            return ProductPriceListID.Value;
        }
    }
}
