using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class ClassAssociateTeacherMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ClassAssociateTeacherMapDTO()
        {
        }

        [DataMember]
        public long ClassAssociateTeacherMapIID { get; set; }

        [DataMember]
        public long? TeacherID { get; set; }

        [DataMember]
        public List<KeyValueDTO> AssociateTeacher { get; set; }



    }
}