using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class NotificationView
    {
        public long NotificationQueueID { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(50)]
        public string NotificationStatusName { get; set; }
        [StringLength(1000)]
        public string ToEmailID { get; set; }
        [StringLength(1000)]
        public string FromEmailID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }
    }
}
