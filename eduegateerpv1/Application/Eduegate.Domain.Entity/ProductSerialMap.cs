namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.ProductSerialMaps")]
    public partial class ProductSerialMap
    {
        [Key]
        public long ProductSerialIID { get; set; }

        [StringLength(200)]
        public string SerialNo { get; set; }

        public long? DetailID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? ProductSKUMapID { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }

        public virtual TransactionDetail TransactionDetail { get; set; }
    }
}
