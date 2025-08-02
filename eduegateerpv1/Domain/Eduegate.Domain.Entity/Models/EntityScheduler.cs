using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("EntitySchedulers", Schema = "schedulers")]
    public partial class EntityScheduler
    {
        [Key]
        public long EntitySchedulerIID { get; set; }
        public Nullable<int> SchedulerTypeID { get; set; }
        public Nullable<int> SchedulerEntityTypeID { get; set; }
        public string EntityValue { get; set; }
        public string EntityID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual SchedulerEntityType SchedulerEntityType { get; set; }
        public virtual SchedulerType SchedulerType { get; set; }
    }
}
