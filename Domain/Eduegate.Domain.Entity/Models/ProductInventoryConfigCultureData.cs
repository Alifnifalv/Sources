using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductInventoryConfigCultureDatas", Schema = "catalog")]
    public partial class ProductInventoryConfigCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        public long ProductInventoryConfigID { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public virtual ProductInventoryConfig ProductInventoryConfig { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
