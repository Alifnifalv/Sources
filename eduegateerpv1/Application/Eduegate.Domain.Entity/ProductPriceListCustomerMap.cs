namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductPriceListCustomerMaps")]
    public partial class ProductPriceListCustomerMap
    {
        [Key]
        public long ProductPriceListCustomerMapIID { get; set; }

        public long? ProductPriceListID { get; set; }

        public long? CustomerID { get; set; }

        public byte? EntitlementID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ProductPriceList ProductPriceList { get; set; }

        public virtual EntityTypeEntitlement EntityTypeEntitlement { get; set; }
    }
}
