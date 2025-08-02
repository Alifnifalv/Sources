using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class TeacherMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
       
        [DataMember]
        public long TeacherMapIID { get; set; }


        [DataMember]
        public long? ClassGroupID { get; set; }

        [DataMember]
        public long? TeacherID { get; set; }


    }
}


