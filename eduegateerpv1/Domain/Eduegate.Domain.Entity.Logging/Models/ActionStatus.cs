using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Logging.Models
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

        public virtual ICollection<Activity> Activities { get; set; }
    }
}