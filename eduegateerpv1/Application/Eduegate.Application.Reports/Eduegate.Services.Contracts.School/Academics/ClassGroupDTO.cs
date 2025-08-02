using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class ClassGroupDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ClassGroupDTO()
        {
            HeadTeacher = new KeyValueDTO();
            ClassTeacher = new List<KeyValueDTO>();
            Subject = new KeyValueDTO();
        }

        [DataMember]
        public long ClassGroupID { get; set; }
        [DataMember]
        public string  GroupDescription { get; set; }

        [DataMember]
        public long? HeadTeacherID { get; set; }

        [DataMember]
        public KeyValueDTO HeadTeacher { get; set; }

        [DataMember]
        public long? ClassTeacherID { get; set; }

        [DataMember]
        public List<KeyValueDTO> ClassTeacher { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }
        [DataMember]
        public KeyValueDTO Subject { get; set; }

    }
}


