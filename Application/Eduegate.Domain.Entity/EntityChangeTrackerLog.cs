namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sync.EntityChangeTrackerLog")]
    public partial class EntityChangeTrackerLog
    {
        public long EntityChangeTrackerLogID { get; set; }

        public long EntityChangeTrackerType { get; set; }

        public long EntityChangeTrackerTypeID { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? SyncedOn { get; set; }
    }
}
