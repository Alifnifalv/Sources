namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.SKUMapBarCodeMaps")]
    public partial class SKUMapBarCodeMap
    {
        [Key]
        public long SKUMapBarCodeMapIID { get; set; }

        public long? SKUMapID { get; set; }

        [StringLength(50)]
        public string BarCode { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
