using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("TrackerStatuses", Schema = "sync")]
    public partial class TrackerStatus
    {
        public TrackerStatus()
        {
            this.EntityChangeTrackers = new List<EntityChangeTracker>();
        }

        [Key]
        public int TrackerStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<EntityChangeTracker> EntityChangeTrackers { get; set; }
    }
}
