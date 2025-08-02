using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("SyncEntities", Schema = "sync")]
    public partial class SyncEntity
    {
        public SyncEntity()
        {
            this.EntityChangeTrackers = new List<EntityChangeTracker>();
        }

        [Key]
        public int EntityID { get; set; }
        public string EntityName { get; set; }
        public string EntityDataSource { get; set; }
        public virtual ICollection<EntityChangeTracker> EntityChangeTrackers { get; set; }
    }
}
