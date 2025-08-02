using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class SubjectTeacherMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SubjectTeacherMapDTO()
        {
            Teachers = new List<KeyValueDTO>();
        }

        [DataMember]
        public long  SubjectTeacherMapIID { get; set; }
        [DataMember]
        public int?  SubjectID { get; set; }
        [DataMember]
        public KeyValueDTO SubjectName { get; set; }
        [DataMember]
        public long?  EmployeeID { get; set; }
        [DataMember]
        public KeyValueDTO EmployeeName { get; set; }
        [DataMember]
        public int?  SectionID { get; set; }
        [DataMember]
        public KeyValueDTO SectionName { get; set; }
        [DataMember]
        public int?  ClassID { get; set; }
        [DataMember]
        public KeyValueDTO ClassName { get; set; }
        [DataMember]
        public string TeacherName { get; set; }
        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public List<KeyValueDTO> Teachers { get; set; }
    }
}


