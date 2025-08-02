using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Leads
{
    [DataContract]
    public class LeadCommunicationDTO : BaseMasterDTO
    {
        [DataMember]
        public long CommunicationIID { get; set; }

        [DataMember]
        public byte? CommunicationTypeID { get; set; }

        [DataMember]
        public int? EmailTemplateID { get; set; }

        [DataMember]
        [StringLength(500)]
        public string EmailCC { get; set; }

        [DataMember]
        [StringLength(500)]
        public string Email { get; set; }

        [DataMember]
        public string EmailContent { get; set; }

        [DataMember]
        [StringLength(50)]
        public string Notes { get; set; }

        [DataMember]
        public DateTime? CommunicationDate { get; set; }

        [DataMember]
        public DateTime? FollowUpDate { get; set; }

        [DataMember]
        public long? LeadID { get; set; }

        [DataMember]
        public string CommunicationType { get; set; }

        [DataMember]
        public string EmailTemplate { get; set; }

        [DataMember]
        public bool IsSendEmail { get; set; }

        [DataMember]
        public string MobileNumber { get; set; }
    }
}


