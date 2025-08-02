using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EntityChangeTrackersQueue", Schema = "sync")]
    public partial class EntityChangeTrackersQueue
    {
        [Key]
        public long EntityChangeTrackerQueueIID { get; set; }
        public long? EntityChangeTrackeID { get; set; }
        public bool? IsReprocess { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [ForeignKey("EntityChangeTrackeID")]
        [InverseProperty("EntityChangeTrackersQueues")]
        public virtual EntityChangeTracker EntityChangeTracke { get; set; }
    }
}
