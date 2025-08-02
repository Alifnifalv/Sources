using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("NotificationUsers", Schema = "notification")]
    public partial class NotificationUser
    {
        [Key]
        public int NotificationUserID { get; set; }
        [Column("NotificationUser")]
        [StringLength(50)]
        public string NotificationUser1 { get; set; }
    }
}
