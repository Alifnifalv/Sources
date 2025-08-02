namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.ProductInventorySerialMaps")]
    public partial class ProductInventorySerialMap
    {
        public long ProductSKUMapID { get; set; }

        public long Batch { get; set; }

        public int? CompanyID { get; set; }

        public long BranchID { get; set; }

        [StringLength(200)]
        public string SerialNo { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? Used { get; set; }

        [Key]
        public long ProductInventorySerialMapIID { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Company Company { get; set; }
    }
}
