using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductInventorySKUConfigMap
    {
        public long ProductInventoryConfigID { get; set; }
        public long ProductSKUMapID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ProductInventoryConfig ProductInventoryConfig { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
