using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SyncEntities", Schema = "sync")]
    public partial class SyncEntity
    {
        public SyncEntity()
        {
            EntityChangeTrackers = new HashSet<EntityChangeTracker>();
        }

        [Key]
        public int EntityID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string EntityName { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string EntityDataSource { get; set; }

        [InverseProperty("Entity")]
        public virtual ICollection<EntityChangeTracker> EntityChangeTrackers { get; set; }
    }
}
