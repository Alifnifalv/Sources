using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class EmailTemplateView
    {
        public int EmailNotificationTypeID { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(500)]
        public string EmailSubject { get; set; }
        [StringLength(200)]
        public string ToCCEmailID { get; set; }
        [StringLength(200)]
        public string ToBCCEmailID { get; set; }
    }
}
