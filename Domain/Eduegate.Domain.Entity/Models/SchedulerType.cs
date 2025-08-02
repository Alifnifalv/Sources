using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("SchedulerTypes", Schema = "schedulers")]
    public partial class SchedulerType
    {
        public SchedulerType()
        {
            this.EntitySchedulers = new List<EntityScheduler>();
        }

        [Key]
        public int SchedulerTypeID { get; set; }
        public string SchedulerTypeName { get; set; }
        public virtual ICollection<EntityScheduler> EntitySchedulers { get; set; }
    }
}
