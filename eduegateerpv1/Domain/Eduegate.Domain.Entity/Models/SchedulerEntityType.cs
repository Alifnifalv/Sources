using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("SchedulerEntityTypes", Schema = "schedulers")]
    public partial class SchedulerEntityType
    {
        public SchedulerEntityType()
        {
            this.EntitySchedulers = new List<EntityScheduler>();
        }

        [Key]
        public int SchedulerEntityTypID { get; set; }
        public string EntityName { get; set; }
        public virtual ICollection<EntityScheduler> EntitySchedulers { get; set; }
    }
}
