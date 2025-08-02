using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductPriceListBranchMaps", Schema = "catalog")]
    public partial class ProductPriceListBranchMap
    {
        [Key]
        public long ProductPriceListBranchMapIID { get; set; }
        public Nullable<long> ProductPriceListID { get; set; }
        public Nullable<long> BranchID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ProductPriceList ProductPriceList { get; set; }
    }
}
