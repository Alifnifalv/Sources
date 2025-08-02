using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SMSNotificationTypes", Schema = "notification")]
    public partial class SMSNotificationType
    {
        [Key]
        public int SMSNotificationTypeID { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [StringLength(500)]
        public string TemplateFilePath { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        public byte[] TimeStamp { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [StringLength(100)]
        public string ModifiedBy { get; set; }
        [StringLength(500)]
        public string Subject { get; set; }
        [StringLength(200)]
        public string ToCC { get; set; }
        [StringLength(200)]
        public string ToBCC { get; set; }
    }
}
