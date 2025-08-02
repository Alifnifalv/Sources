using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TaskAssingners", Schema = "task")]
    public partial class TaskAssingner
    {
        [Key]
        public long TaskAssingedMapIID { get; set; }
        public long? TaskID { get; set; }
        public long? AssingedToLoginID { get; set; }

        [ForeignKey("AssingedToLoginID")]
        [InverseProperty("TaskAssingners")]
        public virtual Login AssingedToLogin { get; set; }
        [ForeignKey("TaskID")]
        [InverseProperty("TaskAssingners")]
        public virtual Task Task { get; set; }
    }
}
