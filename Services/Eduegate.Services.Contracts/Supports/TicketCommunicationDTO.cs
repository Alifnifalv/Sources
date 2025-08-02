using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Supports
{
    public class TicketCommunicationDTO : BaseMasterDTO
    {
        public TicketCommunicationDTO()
        {
        }

        [DataMember]
        public long TicketCommunicationIID { get; set; }

        [DataMember]
        public long? TicketID { get; set; }

        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public DateTime? CommunicationDate { get; set; }

        [DataMember]
        public string CommunicationStringDate { get; set; }

        [DataMember]
        public DateTime? FollowUpDate { get; set; }

        [DataMember]
        public string FollowUpStringDate { get; set; }

        [DataMember]
        public string LoginUserID { get; set; }

        [DataMember]
        public string CommunicationUser { get; set; }

    }
}