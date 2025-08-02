namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sync.EntityChangeTrackersInProcess")]
    public partial class EntityChangeTrackersInProcess
    {
        [Key]
        public long EntityChangeTrackerInProcessIID { get; set; }

        public long? EntityChangeTrackerID { get; set; }

        public bool? IsReprocess { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual EntityChangeTracker EntityChangeTracker { get; set; }
    }
}
