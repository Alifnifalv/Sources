using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class ClassGroupTeacherMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ClassGroupTeacherMapIID { get; set; }

        [DataMember]
        public long? ClassGroupID { get; set; }

        [DataMember]
        public long? TeacherID { get; set; }

        [DataMember]
        public string TeacherName { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

    }
}