using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Activities", Schema = "analytics")]
    public partial class Activity
    {
        [Key]
        public long ActivitiyIID { get; set; }
        public long? UserID { get; set; }
        public int? ActivityTypeID { get; set; }
        public int? ActionTypeID { get; set; }
        public int? ActionStatusID { get; set; }
        [StringLength(500)]
        public string ReferenceID { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Created { get; set; }

        [ForeignKey("ActionStatusID")]
        [InverseProperty("Activities")]
        public virtual ActionStatus ActionStatus { get; set; }
        [ForeignKey("ActionTypeID")]
        [InverseProperty("Activities")]
        public virtual ActionType ActionType { get; set; }
        [ForeignKey("ActivityTypeID")]
        [InverseProperty("Activities")]
        public virtual ActivityType ActivityType { get; set; }
        [ForeignKey("UserID")]
        [InverseProperty("Activities")]
        public virtual UserReference User { get; set; }
    }
}
