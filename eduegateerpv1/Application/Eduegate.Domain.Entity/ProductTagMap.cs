namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductTagMaps")]
    public partial class ProductTagMap
    {
        [Key]
        public long ProductTagMapIID { get; set; }

        public long? ProductTagID { get; set; }

        public long? ProductID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Product Product { get; set; }

        public virtual ProductTag ProductTag { get; set; }
    }
}
