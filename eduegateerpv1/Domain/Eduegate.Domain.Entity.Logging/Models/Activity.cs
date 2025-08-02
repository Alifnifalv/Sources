using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Logging.Models
{
    [Table("Activities", Schema = "analytics")]
    public partial class Activity
    {
        [Key]
        public long ActivitiyIID { get; set; }

        public int? ActivityTypeID { get; set; }

        public int? ActionTypeID { get; set; }

        public int ActionStatusID { get; set; }

        [StringLength(500)]
        public string ReferenceID { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? Created { get; set; }

        public long? UserID { get; set; }

        public virtual ActionStatus ActionStatus { get; set; }

        public virtual ActionType ActionType { get; set; }

        public virtual ActivityType ActivityType { get; set; }
    }
}