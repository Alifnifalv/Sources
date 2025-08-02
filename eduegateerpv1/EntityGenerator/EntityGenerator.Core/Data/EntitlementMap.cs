using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EntitlementMaps", Schema = "mutual")]
    public partial class EntitlementMap
    {
        [Key]
        public long EntitlementMapIID { get; set; }
        public long? ReferenceID { get; set; }
        public bool? IsLocked { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? EntitlementAmount { get; set; }
        public short? EntitlementDays { get; set; }
        public byte? EntitlementID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("EntitlementID")]
        [InverseProperty("EntitlementMaps")]
        public virtual EntityTypeEntitlement Entitlement { get; set; }
    }
}
