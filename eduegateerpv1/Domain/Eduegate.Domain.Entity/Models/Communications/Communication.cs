using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Eduegate.Domain.Entity.Communications
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

        [StringLength(50)]
        public string Notes { get; set; }

        public DateTime? CommunicationDate { get; set; }

        public DateTime? FollowUpDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? LeadID { get; set; }

        [StringLength(500)]
        public string FromEmail { get; set; }

        public string MobileNumber { get; set; }

        public virtual Lead Lead { get; set; }

        public virtual CommunicationType CommunicationType { get; set; }

        public virtual EmailTemplate EmailTemplate { get; set; }
    }
}
