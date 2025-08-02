using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ActionTypes", Schema = "analytics")]
    public partial class ActionType
    {
        public ActionType()
        {
            Activities = new HashSet<Activity>();
        }

        [Key]
        public int ActionTypeID { get; set; }
        [StringLength(100)]
        public string ActionTypeName { get; set; }

        [InverseProperty("ActionType")]
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
