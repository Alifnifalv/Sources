namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductInventoryConfigCultureDatas")]
    public partial class ProductInventoryConfigCultureData
    {
        [Key]
        [Column(Order = 0)]
        public byte CultureID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ProductInventoryConfigID { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        public virtual ProductInventoryConfig ProductInventoryConfig { get; set; }

        public virtual Culture Culture { get; set; }
    }
}
