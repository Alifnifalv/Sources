using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class GetSKUByProperty
    {
        [Key]
        public long ProductIID { get; set; }
        public long ProductSKUMapIID { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public Nullable<int> Sequence { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public string SKUPropertyTypeID { get; set; }
        public string SKUPropertyID { get; set; }
        public string ImageFile { get; set; }
        public Nullable<decimal> SellingQuantityLimit { get; set; }
    }
}
