namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductSKUSiteMap")]
    public partial class ProductSKUSiteMap
    {
        [Key]
        public long ProductSKUSiteMapIID { get; set; }

        public long? ProductSKUMapID { get; set; }

        public int? SiteID { get; set; }

        public bool? IsActive { get; set; }

        public byte? CultureID { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
