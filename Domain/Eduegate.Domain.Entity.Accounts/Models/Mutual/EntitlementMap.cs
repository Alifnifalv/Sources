using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Mutual
{
    [Table("EntitlementMaps", Schema = "mutual")]
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

        //public byte[] TimeStamps { get; set; }

        public virtual EntityTypeEntitlement Entitlement { get; set; }
    }
}