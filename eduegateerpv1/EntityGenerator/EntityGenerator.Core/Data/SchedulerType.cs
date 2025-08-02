using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SchedulerTypes", Schema = "schedulers")]
    public partial class SchedulerType
    {
        public SchedulerType()
        {
            EntitySchedulers = new HashSet<EntityScheduler>();
        }

        [Key]
        public int SchedulerTypeID { get; set; }
        [StringLength(50)]
        public string SchedulerTypeName { get; set; }

        [InverseProperty("SchedulerType")]
        public virtual ICollection<EntityScheduler> EntitySchedulers { get; set; }
    }
}
