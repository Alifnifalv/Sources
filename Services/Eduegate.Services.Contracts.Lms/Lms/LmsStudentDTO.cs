using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Lms
{
    [DataContract]
    public class LmsStudentDTO : BaseMasterDTO
    {
        public LmsStudentDTO()
        {
        }

        [DataMember]
        public long StudentIID { get; set; }

        [DataMember]
        public string AdmissionNumber { get; set; }

        [DataMember]
        public long StudentFullName { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public long? ParentID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }
    }
}