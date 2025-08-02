namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.ProductTypeDeliveryTypeMaps")]
    public partial class ProductTypeDeliveryTypeMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ProductTypeDeliveryTypeMapIID { get; set; }

        public int? ProductTypeID { get; set; }

        public int? DeliveryTypeID { get; set; }

        public int? CompanyID { get; set; }

        public int? Sequence { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual ProductType ProductType { get; set; }

        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }

        public virtual Company Company { get; set; }
    }
}
