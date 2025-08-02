using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SupplierAccountMaps", Schema = "account")]
    public partial class SupplierAccountMap
    {
        [Key]
        public long SupplierAccountMapIID { get; set; }
        public long? SupplierID { get; set; }
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
        [InverseProperty("SupplierAccountMaps")]
        public virtual Account Account { get; set; }
        [ForeignKey("EntitlementID")]
        [InverseProperty("SupplierAccountMaps")]
        public virtual EntityTypeEntitlement Entitlement { get; set; }
        [ForeignKey("SupplierID")]
        [InverseProperty("SupplierAccountMaps")]
        public virtual Supplier Supplier { get; set; }
    }
}
