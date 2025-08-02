using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EntitySchedulers", Schema = "schedulers")]
    public partial class EntityScheduler
    {
        [Key]
        public long EntitySchedulerIID { get; set; }
        public int? SchedulerTypeID { get; set; }
        public int? SchedulerEntityTypeID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string EntityValue { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string EntityID { get; set; }

        [ForeignKey("SchedulerEntityTypeID")]
        [InverseProperty("EntitySchedulers")]
        public virtual SchedulerEntityType SchedulerEntityType { get; set; }
        [ForeignKey("SchedulerTypeID")]
        [InverseProperty("EntitySchedulers")]
        public virtual SchedulerType SchedulerType { get; set; }
    }
}
