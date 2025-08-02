using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductPriceListSupplierMap
    {
        public long ProductPriceListSupplierMapIID { get; set; }
        public Nullable<long> ProductPriceListID { get; set; }
        public Nullable<long> SupplierID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ProductPriceList ProductPriceList { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
