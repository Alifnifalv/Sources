using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class NotificationAlertView
    {
        public long NotificationAlertIID { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(1000)]
        public string Message { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public long? FromLoginID { get; set; }
        public long? ToLoginID { get; set; }
        [Required]
        [StringLength(1)]
        [Unicode(false)]
        public string actionrequired { get; set; }
        [Required]
        [StringLength(1)]
        [Unicode(false)]
        public string Viewdocuments { get; set; }
    }
}
