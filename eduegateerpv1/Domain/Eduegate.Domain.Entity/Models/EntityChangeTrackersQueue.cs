using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("EntityChangeTrackersQueue", Schema = "sync")]
    public partial class EntityChangeTrackersQueue
    {
        [Key]
        public long EntityChangeTrackerQueueIID { get; set; }
        public Nullable<long> EntityChangeTrackeID { get; set; }
        public Nullable<bool> IsReprocess { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public virtual EntityChangeTracker EntityChangeTracker { get; set; }
    }
}
