using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SchedulerEntityTypes", Schema = "schedulers")]
    public partial class SchedulerEntityType
    {
        public SchedulerEntityType()
        {
            EntitySchedulers = new HashSet<EntityScheduler>();
        }

        [Key]
        public int SchedulerEntityTypID { get; set; }
        [StringLength(50)]
        public string EntityName { get; set; }

        [InverseProperty("SchedulerEntityType")]
        public virtual ICollection<EntityScheduler> EntitySchedulers { get; set; }
    }
}
