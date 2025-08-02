using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CustomerAccountMaps", Schema = "account")]
    public partial class CustomerAccountMap
    {
        [Key]
        public long CustomerAccountMapIID { get; set; }
        public long CustomerID { get; set; }
        public long? AccountID { get; set; }
        public byte? EntitlementID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AccountID")]
        [InverseProperty("CustomerAccountMaps")]
        public virtual Account Account { get; set; }
        [ForeignKey("CustomerID")]
        [InverseProperty("CustomerAccountMaps")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("EntitlementID")]
        [InverseProperty("CustomerAccountMaps")]
        public virtual EntityTypeEntitlement Entitlement { get; set; }
    }
}
