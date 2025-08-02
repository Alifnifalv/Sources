using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Communications", Schema = "crm")]
    public partial class Communication
    {
        [Key]
        public long CommunicationIID { get; set; }
        public byte? CommunicationTypeID { get; set; }
        public int? EmailTemplateID { get; set; }
        [StringLength(500)]
        public string EmailCC { get; set; }
        [StringLength(500)]
        public string Email { get; set; }
        public string EmailContent { get; set; }
        [StringLength(1000)]
        public string Notes { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CommunicationDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FollowUpDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? LeadID { get; set; }
        [StringLength(500)]
        public string FromEmail { get; set; }
        public long? ReferenceID { get; set; }
        [StringLength(15)]
        public string MobileNumber { get; set; }

        [ForeignKey("CommunicationTypeID")]
        [InverseProperty("Communications")]
        public virtual CommunicationType CommunicationType { get; set; }
        [ForeignKey("EmailTemplateID")]
        [InverseProperty("Communications")]
        public virtual EmailTemplate EmailTemplate { get; set; }
        [ForeignKey("LeadID")]
        [InverseProperty("Communications")]
        public virtual Lead Lead { get; set; }
    }
}
