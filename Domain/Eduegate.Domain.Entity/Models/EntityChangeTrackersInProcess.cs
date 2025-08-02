using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("EntityChangeTrackersInProcess", Schema = "sync")]
    public partial class EntityChangeTrackersInProcess
    {
        [Key]
        public long EntityChangeTrackerInProcessIID { get; set; }
        public Nullable<long> EntityChangeTrackerID { get; set; }
        public Nullable<bool> IsReprocess { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual EntityChangeTracker EntityChangeTracker { get; set; }
    }
}
