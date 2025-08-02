using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Logging.Models
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

        public virtual ICollection<Activity> Activities { get; set; }
    }
}