namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sync.EntityChangeTrackersQueue")]
    public partial class EntityChangeTrackersQueue
    {
        [Key]
        public long EntityChangeTrackerQueueIID { get; set; }

        public long? EntityChangeTrackeID { get; set; }

        public bool? IsReprocess { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual EntityChangeTracker EntityChangeTracker { get; set; }
    }
}
