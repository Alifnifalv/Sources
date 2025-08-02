namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.EntitlementMaps")]
    public partial class EntitlementMap
    {
        [Key]
        public long EntitlementMapIID { get; set; }

        public long? ReferenceID { get; set; }

        public bool? IsLocked { get; set; }

        public decimal? EntitlementAmount { get; set; }

        public short? EntitlementDays { get; set; }

        public byte? EntitlementID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual EntityTypeEntitlement EntityTypeEntitlement { get; set; }
    }
}
