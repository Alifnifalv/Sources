using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductLocationMaps", Schema = "inventory")]
    public partial class ProductLocationMap
    {
        [Key]
        public long ProductLocationMapIID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<long> LocationID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        public virtual Location Location { get; set; }
    }
}
