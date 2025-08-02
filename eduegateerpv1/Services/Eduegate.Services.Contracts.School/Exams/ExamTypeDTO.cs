using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{

    [DataContract]
    public class ExamTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public byte ExamTypeID { get; set; }
        
        [DataMember]
        public string ExamTypeDescription { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }
    }
}
