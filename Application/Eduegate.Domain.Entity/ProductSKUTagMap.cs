namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductSKUTagMaps")]
    public partial class ProductSKUTagMap
    {
        [Key]
        public long ProductSKUTagMapIID { get; set; }

        public long? ProductSKUTagID { get; set; }

        public long? ProductSKuMapID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CompanyID { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }

        public virtual ProductSKUTag ProductSKUTag { get; set; }
    }
}
