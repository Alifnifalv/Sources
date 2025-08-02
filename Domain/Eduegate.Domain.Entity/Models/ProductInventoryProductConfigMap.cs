using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductInventoryProductConfigMaps", Schema = "catalog")]
    public partial class ProductInventoryProductConfigMap
    {
        [Key]
        public long ProductInventoryConfigID { get; set; }
        public long ProductID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual ProductInventoryConfig ProductInventoryConfig { get; set; }
        public virtual Product Product { get; set; }
    }
}
