using System;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class CommunicationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long CommunicationIID { get; set; }

        [DataMember]
        public long? ReferenceID { get; set; }

        [DataMember]
        public string FromEmail { get; set; }

        [DataMember]
        public string ToEmail { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string EmailContent { get; set; }

        [DataMember]
        public long? ScreenID { get; set; }

        [DataMember]
        public int? EmailTemplateID { get; set; }

        [DataMember]
        public string EmailTemplate { get; set; }

        [DataMember]
        public string MobileNumber { get; set; }

        [DataMember]
        public byte? CommunicationTypeID { get; set; }
        [DataMember]
        public DateTime? FollowUpDate { get; set; }
        [DataMember]
        public string FollowUpDateString { get; set; }

    }
}