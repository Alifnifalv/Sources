using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Lms
{
    [DataContract]
    public class LmsSlotRemarkMapDTO : BaseMasterDTO
    {
        public LmsSlotRemarkMapDTO()
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
    }
}