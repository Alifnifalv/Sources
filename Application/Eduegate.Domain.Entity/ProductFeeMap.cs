namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductFeeMaps")]
    public partial class ProductFeeMap
    {
        [Key]
        public long ProductFeeMapIID { get; set; }

        public int? FeeMasterID { get; set; }

        public long? ProductID { get; set; }

        public long? ProductSKUMapID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamps { get; set; }
    }
}
