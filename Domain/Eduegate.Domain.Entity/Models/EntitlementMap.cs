using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("EntitlementMaps", Schema = "mutual")]
    public partial class EntitlementMap
    {
        [Key]
        public long EntitlementMapIID { get; set; }
        public Nullable<long> ReferenceID { get; set; }
        public Nullable<bool> IsLocked { get; set; }
        public Nullable<decimal> EntitlementAmount { get; set; }
        public Nullable<short> EntitlementDays { get; set; }
        public Nullable<byte> EntitlementID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual EntityTypeEntitlement EntityTypeEntitlement { get; set; }
    }
}
