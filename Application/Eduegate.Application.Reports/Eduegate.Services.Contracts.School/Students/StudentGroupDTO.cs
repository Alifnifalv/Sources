using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class StudentGroupDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int StudentGroupID { get; set; }

        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public int? GroupTypeID { get; set; }
        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }
    }
}
