namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.CustomerSupplierMaps")]
    public partial class CustomerSupplierMap
    {
        [Key]
        public long CustomerSupplierMapIID { get; set; }

        public long? CustomerID { get; set; }

        public long? SupplierID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
