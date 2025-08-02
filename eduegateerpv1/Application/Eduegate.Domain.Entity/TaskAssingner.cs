namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("task.TaskAssingners")]
    public partial class TaskAssingner
    {
        [Key]
        public long TaskAssingedMapIID { get; set; }

        public long? TaskID { get; set; }

        public long? AssingedToLoginID { get; set; }

        public virtual Login Login { get; set; }

        public virtual Task Task { get; set; }
    }
}
