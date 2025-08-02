using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.SignUp.SignUps
{
    [DataContract]
    public class SignupSlotRemarkMapDTO : BaseMasterDTO
    {
        public SignupSlotRemarkMapDTO()
        {
        }

        [DataMember]
        public long SignupSlotRemarkMapIID { get; set; }

        [DataMember]
        public long? SignupSlotAllocationMapID { get; set; }

        [DataMember]
        public long? SignupSlotMapID { get; set; }

        [DataMember]
        public long? SignupID { get; set; }

        [DataMember]
        public string TeacherRemarks { get; set; }

        [DataMember]
        public string ParentRemarks { get; set; }

        [DataMember]
        public DateTime? RemarkEnteredDate { get; set; }

        [DataMember]
        public bool ParentRemarkingTimeExpired { get; set; }
    }
}