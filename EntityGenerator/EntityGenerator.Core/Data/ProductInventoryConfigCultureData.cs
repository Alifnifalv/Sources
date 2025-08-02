using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductInventoryConfigCultureDatas", Schema = "catalog")]
    public partial class ProductInventoryConfigCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        [Key]
        public long ProductInventoryConfigID { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("ProductInventoryConfigCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("ProductInventoryConfigID")]
        [InverseProperty("ProductInventoryConfigCultureDatas")]
        public virtual ProductInventoryConfig ProductInventoryConfig { get; set; }
    }
}
