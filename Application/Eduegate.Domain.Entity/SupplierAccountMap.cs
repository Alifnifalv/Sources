namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.SupplierAccountMaps")]
    public partial class SupplierAccountMap
    {
        [Key]
        public long SupplierAccountMapIID { get; set; }

        public long? SupplierID { get; set; }

        public long? AccountID { get; set; }

        public byte? EntitlementID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Account Account { get; set; }

        public virtual EntityTypeEntitlement EntityTypeEntitlement { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
