using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BrandPriceListMap
    {
        [Key]
        public long BrandPriceListItemMapIID { get; set; }
        public Nullable<long> PriceListID { get; set; }
        public Nullable<long> BrandID { get; set; }
        public Nullable<long> UnitGroundID { get; set; }
        public Nullable<long> CustomerGroupID { get; set; }
        public Nullable<decimal> SellingQuantityLimit { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<byte> SortOrder { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public Nullable<decimal> PricePercentage { get; set; }
        public virtual UnitGroup UnitGroup { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual CustomerGroup CustomerGroup { get; set; }
        public virtual ProductPriceList ProductPriceList { get; set; }
    }
}
