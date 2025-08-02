using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmailNotificationTypes", Schema = "notification")]
    public partial class EmailNotificationType
    {
        public EmailNotificationType()
        {
            EmailNotificationDatas = new HashSet<EmailNotificationData>();
        }

        [Key]
        public int EmailNotificationTypeID { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [StringLength(500)]
        public string EmailTemplateFilePath { get; set; }
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
        public string EmailSubject { get; set; }
        [StringLength(200)]
        public string ToCCEmailID { get; set; }
        [StringLength(200)]
        public string ToBCCEmailID { get; set; }

        [InverseProperty("EmailNotificationType")]
        public virtual ICollection<EmailNotificationData> EmailNotificationDatas { get; set; }
    }
}
