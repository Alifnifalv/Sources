using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("UserReferences", Schema = "analytics")]
    public partial class UserReference
    {
        public UserReference()
        {
            Activities = new HashSet<Activity>();
        }

        [Key]
        public long UserID { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        public byte? AppID { get; set; }

        [ForeignKey("AppID")]
        [InverseProperty("UserReferences")]
        public virtual Application App { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
