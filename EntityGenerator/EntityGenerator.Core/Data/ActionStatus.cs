using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ActionStatuses", Schema = "analytics")]
    public partial class ActionStatus
    {
        public ActionStatus()
        {
            Activities = new HashSet<Activity>();
        }

        [Key]
        public int ActionStatusID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("ActionStatus")]
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
