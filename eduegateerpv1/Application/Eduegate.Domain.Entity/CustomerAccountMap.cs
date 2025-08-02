namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.CustomerAccountMaps")]
    public partial class CustomerAccountMap
    {
        [Key]
        public long CustomerAccountMapIID { get; set; }

        public long CustomerID { get; set; }

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

        public virtual Customer Customer { get; set; }

        public virtual EntityTypeEntitlement EntityTypeEntitlement { get; set; }
    }
}
