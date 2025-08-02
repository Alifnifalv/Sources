using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ActivityTypes", Schema = "analytics")]
    public partial class ActivityType
    {
        public ActivityType()
        {
            Activities = new HashSet<Activity>();
        }

        [Key]
        public int ActivityTypeID { get; set; }
        [StringLength(200)]
        public string ActivityTypeName { get; set; }

        [InverseProperty("ActivityType")]
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
