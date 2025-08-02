namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schedulers.EntitySchedulers")]
    public partial class EntityScheduler
    {
        [Key]
        public long EntitySchedulerIID { get; set; }

        public int? SchedulerTypeID { get; set; }

        public int? SchedulerEntityTypeID { get; set; }

        [StringLength(50)]
        public string EntityValue { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [StringLength(50)]
        public string EntityID { get; set; }

        public virtual SchedulerEntityType SchedulerEntityType { get; set; }

        public virtual SchedulerType SchedulerType { get; set; }
    }
}
