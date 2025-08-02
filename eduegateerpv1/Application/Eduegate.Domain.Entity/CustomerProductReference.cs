namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.CustomerProductReferences")]
    public partial class CustomerProductReference
    {
        [Key]
        public long CustomerProductReferenceIID { get; set; }

        public long? CustomerID { get; set; }

        public long? ProductSKUMapID { get; set; }

        [StringLength(50)]
        public string BarCode { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
