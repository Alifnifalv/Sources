namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductSKUBranchMaps")]
    public partial class ProductSKUBranchMap
    {
        [Key]
        public long ProductSKUBranchMapIID { get; set; }

        public long? ProductSKUID { get; set; }

        public long? BranchID { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
