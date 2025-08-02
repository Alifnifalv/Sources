using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TrackerStatuses", Schema = "sync")]
    public partial class TrackerStatus
    {
        public TrackerStatus()
        {
            EntityChangeTrackers = new HashSet<EntityChangeTracker>();
        }

        [Key]
        public int TrackerStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("TrackerStatus")]
        public virtual ICollection<EntityChangeTracker> EntityChangeTrackers { get; set; }
    }
}
