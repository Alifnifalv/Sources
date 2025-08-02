using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.School.Students;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class BroadcastRecipientsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
  
        public long BroadcastRecipientIID { get; set; }
        [DataMember]


        public long BroadcastID { get; set; }
        [DataMember]

        public long ToLoginID { get; set; }
        [DataMember]

        public DateTime? CreatedDate { get; set; }
        [DataMember]

        public virtual BroadCast Broadcast { get; set; }
        [DataMember]


        public GuardianDTO ParentDetails { get; set; }

        [DataMember]

        public long? StudentID { get; set; }

        [DataMember]

        public string StudentName { get; set; }
    }
}